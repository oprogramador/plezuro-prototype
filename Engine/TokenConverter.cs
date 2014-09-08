using System;
using System.Collections.Generic;
using MyCollections;
using MyTypes.MyClasses;
using System.Linq;

namespace Engine {
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

		private Token matchSquare(List<Token> output) {
			if(lastType<0 || lastType==TokenTypes.Operator || lastType==TokenTypes.BracketOpen) {
				output.Add( new Token(ListFullSymbol, TokenTypes.Symbol, new SoftLink(SymbolMap.ListSymbol)) );
				return new Token("(", TokenTypes.BracketOpen, "(");
			}
			output.Add( new Token(ObjectT.DotSymbol, TokenTypes.Operator, ObjectT.DotSymbol) );
			return new Token(RefFullSymbol, TokenTypes.Symbol, new SoftLink(SymbolMap.RefSymbol));
		}

		private void matchSquares() {
			lastType = (TokenTypes)(-1);		
			var ret = new List<Token>();
			foreach(var t in tokens) {
				var ob = t;
				if(t.Type == TokenTypes.SquareOpen) ob = matchSquare(ret);
				if(t.Type == TokenTypes.SquareClose) ob = new Token(")", TokenTypes.BracketClose, ")");
				if(t.Type != TokenTypes.EndLine) lastType = t.Type;
				ret.Add(ob);
			}
			foreach(var t in ret) Console.WriteLine("[]c="+t);
			tokens = ret;
		}
				

		private void matchFunction(TokenTypes t) {
			Console.WriteLine("last="+lastType+" isvalue="+lastType.IsValue());
			Console.WriteLine("t="+t+" isvalue="+t.IsValue());
			if((t.IsValue() || t==TokenTypes.BracketOpen || t==TokenTypes.CurlyOpen) && (lastType.IsValue() || lastType==TokenTypes.BracketClose))
				Output.Add( ObjectT.FunctionSymbol );
		}

		private object matchEndLine() {
			Console.WriteLine("lastType="+lastType);
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
			foreach(var t in ret) Console.WriteLine("emc="+t);
		}

		private void match() {
			lastType = (TokenTypes)(-1);		
			foreach(var t in tokens) {
				var ob = t.Value;
				switch(t.Type) {
					//case TokenTypes.Operator: 		ob = matchOperator(ref i); 			break;
					//case TokenTypes.Number: 		ob = matchNumber(ref i); 			break;
					//case TokenTypes.Symbol: 		ob = matchSymbol(ref i); 			break;
					//case TokenTypes.BracketOpen:	 	ob = "("; i++;	 				break;
					//case TokenTypes.BracketClose:	 	ob = matchBracketClose(false);			break;
					//case TokenTypes.SquareOpen:	 	ob = matchSquare();				break;
					//case TokenTypes.SquareClose:	 	ob = ")";					break;
					case TokenTypes.EndLine:	 	ob = matchEndLine();		 		break;
				}
				Console.WriteLine("ob="+ob);
				matchFunction(t.Type);
				if(t.Type != TokenTypes.EndLine) lastType = t.Type;
				if(ob!=null) Output.Add( ob );
			}
			foreach(var t in Output) Console.WriteLine("tc="+t);
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
				case TokenTypes.OneString: 		return true;
				case TokenTypes.BiString: 		return true;
				case TokenTypes.TriString: 		return true;
				case TokenTypes.SoftLink: 		return true;
				case TokenTypes.OneLineComment: 	return false;
				case TokenTypes.MultiLineComment: 	return false;
				case TokenTypes.WhiteSpace:	 	return false;
				case TokenTypes.EndLine:	 	return true;
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
