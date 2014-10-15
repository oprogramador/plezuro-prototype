/*
 * Tokenizer.cs
 * Copyright 2014 pierre (Piotr Sroczkowski) <pierre.github@gmail.com>
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301, USA.
 * 
 * 
 */
 

using System;
using System.Collections.Generic;using System.Text;
using System.Text.RegularExpressions;
using Mondo.MyCollections;
using Mondo.MyTypes;
using Mondo.MyTypes.MyClasses;

namespace Mondo.Engine {
	class Tokenizer {
		public readonly Dictionary<int,string> regexes = new Dictionary<int,string> { 
			{16,  	@"^0x[0-9a-f]+"}, 
			{2, 	@"^0b[01]+"}, 
			{8,	@"^0[0-7]+"}, 
			{10, 	@"^[0-9]+(\.[0-9]+)?(e[\+\-]?[0-9]+)?"},
		};

		public static readonly string SoftLinkSymbol = "::";
		public static readonly string OneLineCommentSymbol = "//";
		public static readonly string MultiLineCommentOpenSymbol = "/*";
		public static readonly string MultiLineCommentCloseSymbol = "*/";
		public static readonly string OneStringSymbol = "'";
		public static readonly string BiStringSymbol = "\"";
		public static readonly string TriStringSymbol = "'''";
		public static readonly string DollarSymbol = "$[";
		public static readonly string HashSymbol = "#[";

		private StringBuilder StrB;
		public List<Token> Output { get; private set; }

		public Tokenizer(string str)
		{
			StrB = new StringBuilder(str);
			toTokens();
		}

		private TokenTypes detectType(int i) {
			if(General.Converges( StrB, DollarSymbol, i )) return TokenTypes.DollarOpen;
			if(General.Converges( StrB, HashSymbol, i )) return TokenTypes.HashOpen;
			if(Char.IsLetter(StrB[i]) || "_$".IndexOf(StrB[i])>=0) return TokenTypes.Symbol;
			if(Char.IsDigit(StrB[i])) return TokenTypes.Number;
			//if(StrB[i]=='.') return TokenTypes.Dot;
			if(StrB[i]=='(') return TokenTypes.BracketOpen;
			if(StrB[i]==')') return TokenTypes.BracketClose;
			if(StrB[i]=='[') return TokenTypes.SquareOpen;
			if(StrB[i]==']') return TokenTypes.SquareClose;
			if(StrB[i]=='{') return TokenTypes.CurlyOpen;
			if(StrB[i]=='}') return TokenTypes.CurlyClose;
			if(General.Converges( StrB, SoftLinkSymbol, i )) return TokenTypes.SoftLink;
			if(General.Converges( StrB, OneLineCommentSymbol, i ))  return TokenTypes.OneLineComment;
			if(General.Converges( StrB, MultiLineCommentOpenSymbol, i )) return TokenTypes.MultiLineComment;
			if(General.Converges( StrB, TriStringSymbol, i )) return TokenTypes.TriString;
			if(General.Converges( StrB, BiStringSymbol, i )) return TokenTypes.BiString;
			if(General.Converges( StrB, OneStringSymbol, i )) return TokenTypes.OneString;
			if(Char.IsWhiteSpace(StrB[i])) {
				if(StrB[i]!='\n') return TokenTypes.WhiteSpace;
				return TokenTypes.EndLine;
			}
			return TokenTypes.Operator;
		}


		private string matchOperator(ref int i) {
			string ret = "";
			string prev = "";
			for(;i<StrB.Length && !Char.IsDigit(StrB[i]) && !Char.IsLetter(StrB[i]) && "_$".IndexOf(StrB[i])<0; i++) {
				ret+=StrB[i];
				if(SymbolMap.FullOperSet.Contains(ret)) prev = ret;
			}
			i -= ret.Length-prev.Length;
			return prev;
		}

		private string matchSymbol(ref int i) {
			//if(isFunctionHere) Output.Add(ObjectT.FunctionSymbol);
			string ret = ""+StrB[i++];
			for(;i<StrB.Length && (StrB[i]=='_' || Char.IsLetterOrDigit(StrB[i])); i++) {
				ret += StrB[i];
			}
			return ret;
		}

		private object matchSoftLink(ref int i) {
			string ret = "";
			i += SoftLinkSymbol.Length;
			for(;i<StrB.Length && (StrB[i]=='_' || Char.IsLetterOrDigit(StrB[i])); i++) {
				ret += StrB[i];
			}
			return new SoftLink(ret);
		}

		/*private string matchDot(ref int i) {
			dot = true;
			i++;
			return ".";
		}*/

		private string matchStrBNumber(ref int i) {
			string ret = ""+StrB[i];
			for(int j=1; i+j<StrB.Length && (Char.IsLetterOrDigit(StrB[i+j]) || StrB[i+j]=='.' || ("+-".IndexOf(StrB[i+j])>=0 && StrB[i+j-1]=='e')); j++)
				ret += StrB[i+j];
			return ret;
		}

		private Number matchNumber(ref int i) {
			//if(isFunctionHere) Output.Add(ObjectT.FunctionSymbol);
			string numstr = matchStrBNumber(ref i); 
			int maxlen=0;
			int keymax=0;
			string strmax = null;
			foreach(var key in regexes.Keys) {
				var match = new Regex(regexes[key]).Match(numstr);
				if(match.Length > maxlen) {
					maxlen = match.Length;
					keymax = key;
					strmax = match.Value;
				}
			}
			i += maxlen;
			if(maxlen==0) throw new SyntaxException(TokenTypes.Number);
			return new Number(StaticParser.ParseUniv(strmax,keymax));
		}

		private StringT matchString(ref int i, string delim, bool multiline) {
			//if(isFunctionHere) Output.Add(ObjectT.FunctionSymbol);
			string ret = "";
			i += delim.Length;
			for(;i<StrB.Length; i++) {
				if(StrB.Length-1 > i) {
					string ch = ""+StrB[i]+StrB[i+1];
					if(StringT.escapeChars.ContainsKey(ch)) {
						ret += StringT.escapeChars[ch];
						i++;
						continue;
					}
				}
				if(General.Converges(StrB, delim, i)) break;
				ret += multiline || StrB[i]!='\n' ? ""+StrB[i] : "";
			}
			i += delim.Length;
			return new StringT(ret);
		}

		private object matchOneLineComment(ref int i) {
			for(; i<StrB.Length && StrB[i]!='\n'; i++);
			i++;
			return new NullArg();
		}

		private object matchMultiLineComment(ref int i) {
			for(; i<StrB.Length; i++) {
				if(General.Converges(StrB,"*/",i)) break;
			}
			i+=2;
			return new NullArg();
		}

		private object matchWhiteSpace(ref int i) {
			for(; i<StrB.Length && Char.IsWhiteSpace(StrB[i]); i++);
			return new NullArg();
		}

		private Token match(ref int i, TokenTypes t) {
			object ob = null;
			int beg = i;
			switch(t) {
				case TokenTypes.Operator: 		ob = matchOperator(ref i); 			break;
				case TokenTypes.Number: 		ob = matchNumber(ref i); 			break;
				case TokenTypes.Symbol: 		ob = matchSymbol(ref i); 			break;
				case TokenTypes.BracketOpen:	 	ob = "("; i++;	 				break;
				case TokenTypes.BracketClose:	 	ob = ")"; i++;					break;
				case TokenTypes.SquareOpen:	 	ob = "["; i++;					break;
				case TokenTypes.SquareClose:	 	ob = "]"; i++;					break;
				case TokenTypes.CurlyOpen:	 	ob = "{"; i++;					break;
				case TokenTypes.CurlyClose:	 	ob = "}"; i++;					break;
				case TokenTypes.DollarOpen:	 	ob = DollarSymbol; i+=DollarSymbol.Length;	break;
				case TokenTypes.HashOpen:	 	ob = HashSymbol; i+=HashSymbol.Length;		break;
				case TokenTypes.OneString: 		ob = matchString(ref i, OneStringSymbol, false);break;
				case TokenTypes.BiString: 		ob = matchString(ref i, BiStringSymbol, false);	break;
				case TokenTypes.TriString: 		ob = matchString(ref i, TriStringSymbol, true);	break;
				case TokenTypes.SoftLink: 		ob = matchSoftLink(ref i);		 	break;
				case TokenTypes.OneLineComment: 	ob = matchOneLineComment(ref i); 		break;
				case TokenTypes.MultiLineComment: 	ob = matchMultiLineComment(ref i); 		break;
				case TokenTypes.WhiteSpace:	 	ob = matchWhiteSpace(ref i);	 		break;
				case TokenTypes.EndLine:	 	ob = "\n"; i++;			 		break;
			}
			if(ob==null) throw new SyntaxException();
			//matchFunction(t);
			//matchEmpty(t);
			if(ob is string) if((string)ob == "") throw new SyntaxException();
			return new Token(StrB.ToString(beg , Math.Min(i-beg, StrB.Length-beg)), t, ob);
		}

		private void toTokens() {
			try {
				Output = new List<Token>();
				for(int i=0; i<StrB.Length; ) {
					TokenTypes t = detectType(i);
					var ob = match(ref i,t);
					Output.Add(ob);
					//if(!(t==TokenTypes.WhiteSpace || t==TokenTypes.OneLineComment || t==TokenTypes.MultiLineComment)) lastType = t;
				}
				//dotCpl((TokenTypes)(-1));
			} catch{ throw; }
		}
	}
}
