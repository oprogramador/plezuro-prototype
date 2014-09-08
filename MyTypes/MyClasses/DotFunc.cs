using System;
using System.Collections.Generic;
using MyCollections;
using lib;

namespace MyTypes.MyClasses {
	class DotFunc : IVariable, ICallable  {
		public IVariable FirstArg { 
			get;
			private set; 
		}

		public ICallable Proc { get; private set; }

		public DotFunc(ICallable p,IVariable arg) {
			ID = ObjectContainer.Instance.Add(this);
			Proc = p;
			FirstArg = arg;
		}

		public DotFunc(ICallable p) {
			ID = ObjectContainer.Instance.Add(this);
			Proc = p;
		}

		public object Call(IPrintable p, object[] argss) {
			Console.WriteLine("dotfunc call");
			Console.WriteLine("args.len="+argss.Length);
			foreach(var i in General.Shift(argss, FirstArg)) Console.WriteLine("i: "+i);
			return Proc.Call( p, General.Shift(argss, FirstArg) ); 
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is DotFunc) Proc.CompareTo(((DotFunc)ob).Proc);
			return 0;
		}

		public object Clone() {
			return new DotFunc(Proc);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			ObjectT.FunctionSymbol,	(Func<IPrintable,DotFunc,ITuplable,object>) ((p,f,a) => { Co.WL("dotlambda a="+a); return f.Call(p, a.ToArray());}),
		};
		
		static DotFunc() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "DotFunc", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(DotFunc) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "dotFunc: "+Proc+(FirstArg==null ? "" : " firstArg: "+FirstArg);
		}
	}
}
