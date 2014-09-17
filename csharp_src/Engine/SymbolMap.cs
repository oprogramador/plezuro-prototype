/*
 * SymbolMap.cs
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
using System.Collections.Concurrent;
using System.Linq;
using MyCollections;
using MyTypes;
using MyTypes.MyClasses;
using Maths;


namespace Engine {

	class SymbolMap : ConcurrentDictionary<string, object> {


		public static readonly string MainDelimiter = ";";
		public static readonly string EmptySymbol = "empty";
		public static readonly string ListSymbol = "array";
		public static readonly string RefSymbol = "ref";
		public static readonly string ArgsSymbol = "args";

		private static object[] lambdas = {
			"clear",	(Func<IPrintable,object>) ((p) => p.Clear() ),
		};

		private static readonly List<object> Symbols = new List<object>();
			/*
			"oper",		(Func<string,object>) ((x) => Engine.GetInstance().SymbolMap[x]),
			"operd",	(Func<string,Type,object>) ((x,t) => ((FuncDictionary)Engine.GetInstance().SymbolMap[x])[t]),
			"for",		(Func<IPrintable,TupleT,ProcedureT,object>)
					((p, up, o) => { object ret=Evaluator.Eval((ProcedureT)up[0],p); 
						while(Evaluator.Eval((ProcedureT)up[1],p).Equals(true)){ ret=Evaluator.Eval(o,p); Evaluator.Eval((ProcedureT)up[2],p); }
						return ret; }),
			"each",		new FuncDictionary(3) {
					{typeof(ListT), (Func<IPrintable,ListT,ProcedureT,object>)
						((p, ar, f) => 
						{ object ret=new NullType(); int nr=0;
						foreach(IVariable i in ar)
						{ ret=Evaluator.Eval(f,p,new TupleT(new object[]{i,new ReferenceT(new Number(nr++))}));} 
						return ret; } )
					},
					{typeof(SetT), (Func<IPrintable,SetT,ProcedureT,object>)
						((p, ar, f) => 
						{ object ret=new NullType(); int nr=0;
						foreach(IVariable i in ar)
						{ ret=Evaluator.Eval(f,p,new TupleT(new object[]{i,new ReferenceT(new Number(nr++))}));} 
						return ret; } )
					},
					{typeof(DictionaryT), (Func<IPrintable,DictionaryT,ProcedureT,object>)
						((p, ar, f) => 
						{ object ret=new NullType(); int nr=0;
						foreach(var i in ar)
						{ ret=Evaluator.Eval(f,p,new TupleT(
							new object[]{new ReferenceT(new PairT(i.Key,i.Value)),new ReferenceT(new Number(nr++))}
						));} 
						return ret; } )
					},
					},
			*/

		public static void AddSymbols(List<object> sym) {
			Console.WriteLine("symbols");
			foreach(var i in sym) Console.WriteLine("sym: "+i);
			foreach(var i in sym) Symbols.Add(i);
		}

		private static readonly object[] BiOpers = {
			";",	
			":=",
			"=",
			",", 	
			"<->",
			"<<",
			">>",	
			"?",	
			":",	
			"|",	
			"&",	
			"<=>",	
			">=",	
			">",	
			"<=",	
			"<",	
			"!=",	
			"==",	
			"===",	
			"+",
			"-",	
			"%",	
			"*",
			"/",	
			"^",
			new object[] {
				ObjectT.FunctionSymbol,
				ObjectT.DotSymbol,
			},	
		};

		private static readonly string[] UniOpers = {
			"!",	
			"&&",	
			"**",	
			"#",	
			"++",	
			"--",	
		};

		public static readonly HashSet<string> BiOperSet;
		public static readonly HashSet<string> UniOperSet;
		public static readonly HashSet<string> FullOperSet;

		public static HashSet<string> RightToLeft = new HashSet<string>() {
			"^",
			"^^",
		};

		private static readonly Dictionary<string,int> OperPriorityDic;

		public static int OperPriority(string str) {
			try {
				return OperPriorityDic[str];
			} catch {
				return -1;
			}
		}

		static SymbolMap() {
			/*
			Number priv_num = new Number(0);
			BooleanT priv_bool = new BooleanT(true);
			EmptyT priv_empty = new EmptyT();
			NullType priv_null = new NullType();
			*/

			AddSymbols(VariableFactory.GetInstance().Constants);

			OperPriorityDic = new Dictionary<string,int>();
			BiOperSet = new HashSet<string>();
			UniOperSet = new HashSet<string>();
			for(int i=0; i<BiOpers.Length; i++) {
				object[] inlist = BiOpers[i] as object[];
				if(inlist != null) {
					for(int k=0; k<inlist.Length; k++) {
						OperPriorityDic.Add((string)inlist[k], i);
						BiOperSet.Add((string)inlist[k]);
					}
					continue;
				}
				OperPriorityDic.Add((string)BiOpers[i], i);
				BiOperSet.Add((string)BiOpers[i]);
			}
			foreach(var oper in UniOpers) {
				OperPriorityDic.Add(oper, int.MaxValue);
				UniOperSet.Add(oper);
			}
			FullOperSet = new HashSet<string>(UniOperSet.Union(BiOperSet));
		}

		public static readonly string AssignOper = "=";

		public static readonly Dictionary<string,string> ShortOpers = new Dictionary<string,string>() {
			{"+=",	"+"},
			{"-=",	"-"},
			{"*=",	"*"},
			{"/=",	"/"},
			{"%=",	"%"},
			{"&=",	"&"},
			{"|=",	"|"},
			{"^=",	"^"},
			{".=",	"."},
		};

		public static readonly Dictionary<object,object> OpersAtBegDefaultValues = new Dictionary<object,object>() {
			{"-",	0},
		};

		public static readonly List<object> OpersToBeg = new List<object>() {
			",",
			";"
		};

		public SymbolMap() {
			Console.WriteLine("SymbolMap ctor");
			for(int i=0; i<Symbols.Count; i+=2) {
				Console.WriteLine("sym: "+Symbols[i]);
				Add((string)Symbols[i], TypeTrans.toRef( TypeTrans.toMyType(Symbols[i+1])) );
			}
		}
/*
		public void addList(object[] list) {
			for(int i=0; i<list.Length; i+=2) {
				object[] inlist = list[i] as object[];
				if(inlist != null) {
					for(int k=0; k<inlist.Length; k+=2) {
						Add((string)inlist[k], inlist[k+1]);
					}
					i--;
					continue;
				}
				Add((string)list[i], list[i+1]);
			}
		}

		private List<string> makeList(object[] list) {
			var ret = new List<string>();
			for(int i=0; i<list.Length; i+=2) {
				object[] inlist = list[i] as object[];
				if(inlist != null) {
					for(int k=0; k<inlist.Length; k+=2) {
						ret.Add((string)inlist[k]);
						Add((string)inlist[k], inlist[k+1]);
					}
					i--;
					continue;
				}
				ret.Add((string)list[i]);
				Add((string)list[i], list[i+1]);
			}
			return ret;
		}
*/	
	}
}
