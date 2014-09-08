using System;
using System.Collections.Generic;using MyCollections;
using MyTypes;
using MyTypes.MyClasses;

namespace Engine {
	class RPN {
		public ProcedureT Output { get; private set; }

		private Stack<string> opers;
		private List<object> tokens;	
	
		public RPN(List<object> tokens) {
			try {
				this.tokens = tokens;
				Output = new ProcedureT();
				opers = new Stack<string>();
				process();
			} catch{ throw; }
		}
		
		private RPNTypes detectRPNType(object ob) {
			if(ob is Number) return RPNTypes.Constant;
			if(ob is StringT) return RPNTypes.Constant;
			if(ob is SoftLink) return RPNTypes.Constant;
			string str = (string)ob;
			if(Char.IsDigit(str[0])) return RPNTypes.Constant;
			if(Char.IsLetter(str[0]) || "_$".IndexOf(str[0])>=0) return RPNTypes.Symbol;
			if(str=="(") return RPNTypes.BracketOpen;
			if(str==")") return RPNTypes.BracketClose;
			if(str=="{") return RPNTypes.CurlyOpen;
			if(str=="}") return RPNTypes.CurlyClose;
			return RPNTypes.Operator;
		}

		private void pushOper(object token,int i) {
			if(SymbolMap.OpersAtBegDefaultValues.ContainsKey(token)) {
				object val = SymbolMap.OpersAtBegDefaultValues[token];
				if(i==0) Output.Push(val);
				else if(SymbolMap.OpersToBeg.Contains(tokens[i-1])) Output.Push(val);
				else if(tokens[i-1] is string) if(((string)tokens[i-1])=="(") Output.Push(val);
			}
			while(opers.Count>0) {
				if(SymbolMap.OperPriority( opers.Peek() ) < SymbolMap.OperPriority( (string)token )
					|| ( opers.Peek() == (string)token
						&& SymbolMap.RightToLeft.Contains((string)token) )
					|| opers.Peek().EndsWith("(")) break;
				pushCallable();
			}
			opers.Push((string)token);
		}
		
		private void pushCallable() {
			string op = opers.Pop();
			Output.Push( new SoftLink(op) );
			Output.Push( new StopPoint( SymbolMap.UniOperSet.Contains(op) ? 1 : 2 ) );
		}

		private void emptyStack() {
			//bool empty = true;
			while(opers.Count>0) {
				if(opers.Peek()=="(") {
					//if(empty) Output.Push(SymbolMap.EmptySymbol);
					opers.Pop();
					break;
				}
				//empty = false;
				pushCallable();
			}
		}

		private void pushRecursive(ref int i) {
			var list = new List<object>();
			int bra = 1;
			i++;
			for(;i<tokens.Count; i++) {
				if(detectRPNType(tokens[i])==RPNTypes.CurlyOpen) bra++;
				if(detectRPNType(tokens[i])==RPNTypes.CurlyClose) bra--;
				if(bra==0) break;
				list.Add(tokens[i]);
			}
			Output.Push(new RPN(list).Output);
		}

		private void process() {
			try {
				for(int i=0; i<tokens.Count; i++) {
					RPNTypes t = detectRPNType(tokens[i]);
					switch(t) {
						case RPNTypes.Constant: 	Output.Push(tokens[i]); 	break;
						case RPNTypes.Symbol:		Output.Push(tokens[i]); 	break;
						case RPNTypes.Operator:		pushOper(tokens[i],i);		break;
						case RPNTypes.BracketOpen:	opers.Push((string)tokens[i]);	break;
						case RPNTypes.BracketClose:	emptyStack();			break;
						case RPNTypes.CurlyOpen:	pushRecursive(ref i);		break;
						case RPNTypes.CurlyClose:					break;
					}
				}
				while(opers.Count>0) emptyStack();
			} catch{ throw; }
		}
	}
}
