/*
 * RPN.cs
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
using System.Collections.Generic;using Mondo.MyCollections;
using Mondo.MyTypes;
using Mondo.MyTypes.MyClasses;

namespace Mondo.Engine {
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
				object val = TypeTrans.toMyType( SymbolMap.OpersAtBegDefaultValues[token] );
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
