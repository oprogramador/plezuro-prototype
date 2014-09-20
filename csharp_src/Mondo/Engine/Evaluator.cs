/*
 * Evaluator.cs
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
 

using System.Diagnostics;
using System;
using System.Collections.Generic;
using Mondo.MyCollections;
using Mondo.MyTypes;
using Mondo.MyTypes.MyClasses;

namespace Mondo.Engine {
	class Evaluator : IPrintable, IEvalable {
		private WStack<object> list;
		private ProcedureT output;
		
		public ITuplable Args { get; private set; }

		public ConcurrentDictionary<string,object> LocalVars { get; private set; }

		public IPrintable Parent { get; private set; }

		public ITextable Stream { get; private set; }
		private SymbolMap map;

		public IVariable Result { get; private set; }

		public Evaluator(ProcedureT list, IPrintable p, ITuplable args)  {
			try {
				Parent = p;
				Args = args;
				Stream = p.Stream;
				init(list);
			} catch{ throw; }
		}

		public Evaluator(ProcedureT list, ITextable stream, ITuplable args) {
			try {
				Args = args;
				Stream = stream;
				init(list);
			} catch{ throw; }
		}

		private void init(ProcedureT list) {
			try {
				if(list.Count==0) { 
					Result = new EmptyT();
					return;
				}
				LocalVars = new ConcurrentDictionary<string,object>();
				LocalVars.Add( SymbolMap.ArgsSymbol, Args);
				this.list = list.Reverse();
				output = new ProcedureT();
				map = Engine.GetInstance().SymbolMap;
				process();
			} catch{ throw; }

		}

		public Evaluator(ProcedureT p, ITextable t) : this(p,t,new EmptyT()) {
		}

		public static object Eval(ProcedureT list, IPrintable pr) {
			return new Evaluator(list, pr, pr.Args).Result;
		}
	
		public object EvalDyn(ProcedureT list, IPrintable pr, ITuplable args) {
			return new Evaluator(list, pr, args).Result;
		}	

		public object EvalDyn(ProcedureT list, IPrintable pr) {
			return new Evaluator(list, pr, pr.Args).Result;
		}

		public static object Eval(ProcedureT list, IPrintable pr, ITuplable args) {
			return new Evaluator(list, pr, args).Result;
		}

		public static object Eval(ProcedureT list, ITextable stream, ITuplable args) {
			return new Evaluator(list, stream, args).Result;
		}

		public static object Eval(ProcedureT list, ITextable stream) {
			return new Evaluator(list, stream).Result;
		}

		public bool Clear() {
			LocalVars = new ConcurrentDictionary<string,object>();
			return true;
		}	
	
		public void Print(object ob) {
			if(Stream!=null) Stream.Text += ob;
			else Console.Write(ob);
		}
		
		public void PrintLine(object ob) {
			if(Stream!=null) Stream.Text += ""+ob+"\n";
			else Console.WriteLine(ob);
		}

		void funcMatch(int argnr) {
			Console.WriteLine("fm");
			string oper = ((SoftLink)output.Pop()).Value;
			Console.WriteLine("oper="+oper);
			object[] args = new object[argnr];
			Console.WriteLine("argnr="+argnr);
			Console.WriteLine("output="+output);
			for(int i=argnr-1; i>=0; i--) args[i] = TypeTrans.dereference(output.Pop());
			Console.WriteLine("args="+General.EnumToString(args));
			var proc = ClassT.GetClass(args).GetMethod(oper).Proc;
			output.Push( proc.Call(this,args) );
			Console.WriteLine("called");
/*	
			
			if(token is Delegate) {
				var argt = token.GetType().GetGenericArguments();
				var args = new WList<object>();
				var argnr = argt.Length-1;
				if(argt[0] == typeof(IPrintable)) argnr--;
				for(int i=0; i<argnr; i++) args.Shift(output.Pop());
				token = new CallFunc(new BuiltinFunc((Delegate)token), new TupleT(args));
			}
			if(token is FuncDictionary) {
				var args = new WList<object>();
				var argnr = ((FuncDictionary)token).Argnr;
				for(int i=0; i<argnr; i++) args.Shift(output.Pop());
				object f = null;
				try {
					f = TypeTrans.fromDic(token,args[0]);
				} catch {
					args[0] = TypeTrans.tryCall( args[0], this );
					f = TypeTrans.fromDic(token,args[0]);
				}
				token = new CallFunc(new BuiltinFunc((Delegate)f), new TupleT(args));
			}
			if(!(token is CallFunc)) token = new CallFunc((ICallable)token);
			output.Push(((CallFunc)token).Call(this));
		*/		
		}

		void shortOperMatch(string skey) {
/*
			var aa = output.NthElement(1);
			object item = map[SymbolMap.ShortOpers[skey]];
			Console.WriteLine("aa="+aa+" item="+item+" output="+output);
			output.Push(item);
			funcMatch();
			item = map[SymbolMap.AssignOper];
			output.Insert(1,aa);
			output.Push(item);
			funcMatch();
			list.Pop();
*/
		}

		private object findVar(string key) {
			object item = null;
			IPrintable pri = this;
			while(item==null) {
				try {
					item = pri.LocalVars[key];
				} catch {
					pri = pri.Parent;
					if(pri==null) return map[key];
				}
			}
			return item;
		}

		void internProcess() {
			try {
				object key = list.Pop();
				Console.WriteLine("intern key="+key);
				string skey = key as string;
				if(skey!=null) {
					object item = null;
					if(skey.StartsWith("$"))  {
						skey = skey.TrimStart('$');
						try {
							LocalVars.TryAdd(skey, new ReferenceT(new NullType())); 
						} catch(Exception e) { Console.WriteLine("e: "+e); }
					}
					try {
						item = findVar(skey);
					} catch {
						try {
							shortOperMatch(skey);
							return;
						} catch {
							throw new SymbolException(skey);
						}
					}
					output.Push(item);
				}
				else if(key is StopPoint) {
					funcMatch( ((StopPoint)key).Argnr );
				}
				else output.Push(key);
			} catch(Exception e) {
				output.Push(new ErrorT(e));
			}
		}

		void process() {
			while(list.Count>0) {
				internProcess();
			}
			Result = TypeTrans.toMyType( /*TypeTrans.tryCall(*/ TypeTrans.dereference(output.Pop())/*, this)*/ );
		}
		
		public int CompareTo(object ob) {
			int pre = ClassT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return this;
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass = new BuiltinClass( "Evaluator", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(Evaluator) ); 

		private static readonly Dictionary<string,Method> myMethods = new Dictionary<string,Method>() {

		};

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public int ID { get; private set; }

		public override string ToString() {
			return "evaluator (args: "+Args+" local: "+General.EnumToString(LocalVars)+" code: "+list+")";
		}
	}
}
