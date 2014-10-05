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
	class Evaluator : IPrintable {
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
				Stream = p!=null ? p.Stream : null;
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
			return new Evaluator(list, pr, pr!=null ? pr.Args : new EmptyT()).Result;
		}
	
		public object EvalDyn(ProcedureT list, ITuplable args) {
			return new Evaluator(list, this, args).Result;
		}	

		public object EvalDyn(ProcedureT list) {
			return new Evaluator(list, this, Args).Result;
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

		void methodMatch(object outp, object[] args) {
			string oper = ((SoftLink)outp).Value;
			var proc = ClassT.GetClass(args).GetMethod(oper).Proc;
			output.Push( proc.Call(this,args) );
		}

		void staticFuncMatch(object outp, object[] args) {
			var proc = (ICallable)outp;
			output.Push( proc.Call(this,args) );
		}

		void funcMatch(int argnr) {
			var outp = TypeTrans.dereference(output.Pop());
			var args = new List<object>();
			int i=0;
			try {
				for(i=argnr-1; i>=0; i--) args.Insert(0,output.Pop());
			} catch {
				for(; i>=0; i--) args.Add(new EmptyT());
			}
			if(outp is SoftLink) methodMatch(outp, args.ToArray());
			if(outp is ICallable) staticFuncMatch(outp, args.ToArray());
		}

		void shortOperMatch(string skey) {
/*
			var aa = output.NthElement(1);
			object item = map[SymbolMap.ShortOpers[skey]];
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
		
		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
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

		private static ClassT myClass;
		public const string ClassName = "Evaluator";

		private static object[] lambdas = {

		};
		
		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(NullType) );
			return myClass;
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
