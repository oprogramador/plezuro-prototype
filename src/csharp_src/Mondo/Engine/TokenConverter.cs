/*
 * TokenConverter.cs
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
using System.Collections.Generic;
using Mondo.MyCollections;
using Mondo.MyTypes.MyClasses;
using System.Linq;

namespace Mondo.Engine {
	class TokenConverter {
		private List<Token> tokens;
		public List<object> Output { get; private set; }
		public static readonly string ListFullSymbol = Tokenizer.SoftLinkSymbol + SymbolMap.ListSymbol;
		public static readonly string RefFullSymbol = Tokenizer.SoftLinkSymbol + SymbolMap.RefSymbol;

		private TokenTypes lastType = (TokenTypes)(-1);		

		public TokenConverter(List<Token> l) {
			tokens = l;
			Output = new List<object>();
			removeUnnecessary();
			matchSquares();
			matchEmpty();
			match();
		}

		private Token matchFunc(List<Token> output, string symbol) {
			output.Add( new Token(Tokenizer.SoftLinkSymbol+symbol, TokenTypes.Symbol, new SoftLink(symbol)) );
			return new Token("(", TokenTypes.BracketOpen, "(");
		}

		private Token matchSquare(List<Token> output) {
			if(lastType<0 || 
					lastType==TokenTypes.Operator || 
					lastType==TokenTypes.BracketOpen || 
					lastType==TokenTypes.SquareOpen || 
					lastType==TokenTypes.CurlyOpen) {
				output.Add( new Token(ListFullSymbol, TokenTypes.Symbol, new SoftLink(SymbolMap.ListSymbol)) );
				return new Token("(", TokenTypes.BracketOpen, "(");
			}
			output.Add( new Token(ObjectT.DotSymbol, TokenTypes.Operator, ObjectT.DotSymbol) );
			output.Add(new Token(RefFullSymbol, TokenTypes.Symbol, new SoftLink(SymbolMap.RefSymbol)));
			return new Token("(", TokenTypes.BracketOpen, "(");
		}

		private void matchSquares() {
			lastType = (TokenTypes)(-1);		
			var ret = new List<Token>();
			foreach(var t in tokens) {
				var ob = t;
				if(t.Type == TokenTypes.SquareOpen) ob = matchSquare(ret);
				if(t.Type == TokenTypes.DollarOpen) ob = matchFunc(ret, SymbolMap.SetSymbol);
				if(t.Type == TokenTypes.HashOpen) ob = matchFunc(ret, SymbolMap.DicSymbol);
				if(t.Type == TokenTypes.SquareClose) ob = new Token(")", TokenTypes.BracketClose, ")");
				if(t.Type != TokenTypes.EndLine) lastType = t.Type;
				ret.Add(ob);
			}
			tokens = ret;
		}
				

		private void matchFunction(TokenTypes t) {
			if((t.IsValue() || t==TokenTypes.BracketOpen || t==TokenTypes.CurlyOpen) && (lastType.IsValue() || lastType==TokenTypes.BracketClose))
				Output.Add( ObjectT.FunctionSymbol );
		}

		private object matchEndLine() {
			if(lastType==TokenTypes.Operator || lastType==TokenTypes.BracketOpen || lastType==TokenTypes.SquareOpen) return null;
			return SymbolMap.MainDelimiter;
		}

		private void matchEmpty() {
			var ret = new List<Token>();
			foreach(var t in tokens) {
				if(t.Type == TokenTypes.BracketClose && lastType == TokenTypes.BracketOpen)
					//(t.Type == TokenTypes.SquareClose && lastType == TokenTypes.SquareOpen))
						ret.Add( new Token(SymbolMap.EmptySymbol, TokenTypes.Symbol, SymbolMap.EmptySymbol) );
				ret.Add(t);
				if(t.Type != TokenTypes.EndLine) lastType = t.Type;
			}
			tokens = ret;
		}

		private void match() {
			lastType = (TokenTypes)(-1);		
			foreach(var t in tokens) {
				var ob = t.Value;
				matchFunction(t.Type);
				if(t.Type != TokenTypes.EndLine) lastType = t.Type;
				if(ob!=null) Output.Add( ob );
			}
		}

		private bool isNeeded(TokenTypes t)
		{
			switch(t) {
				case TokenTypes.Operator: 		return true;
				case TokenTypes.Number: 		return true;
				case TokenTypes.Symbol: 		return true;
				case TokenTypes.BracketOpen:	 	return true;
				case TokenTypes.BracketClose:	 	return true;
				case TokenTypes.SquareOpen:	 	return true;
				case TokenTypes.SquareClose:	 	return true;
				case TokenTypes.CurlyOpen:	 	return true;
				case TokenTypes.CurlyClose:	 	return true;
				case TokenTypes.DollarOpen:	 	return true;
				case TokenTypes.HashOpen:	 	return true;
				case TokenTypes.OneString: 		return true;
				case TokenTypes.BiString: 		return true;
				case TokenTypes.TriString: 		return true;
				case TokenTypes.SoftLink: 		return true;
				case TokenTypes.OneLineComment: 	return false;
				case TokenTypes.MultiLineComment: 	return false;
				case TokenTypes.WhiteSpace:	 	return false;
				case TokenTypes.EndLine:	 	return false;
			}
			return false;
		}

		private void removeUnnecessary()
		{
			tokens = 
				(from t in tokens
				where isNeeded(t.Type)
				select t).ToList();
		}
	}
}
