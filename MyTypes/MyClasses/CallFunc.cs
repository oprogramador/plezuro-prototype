using System;
using System.Collections.Generic;
using MyCollections;

namespace MyTypes.MyClasses {
	class CallFunc : IVariable {
		public TupleT Args { 
			get; 
			private set; 
		}

		public ICallable Proc { get; private set; }

		public CallFunc(ICallable p) {
			ID = ObjectContainer.Instance.Add(this);
			Proc = p;
		}

		public CallFunc(ICallable p, TupleT args) {
			ID = ObjectContainer.Instance.Add(this);
			Proc = p;
			Args = args;
		}

		public CallFunc Push(object ob) {
			Console.WriteLine("tomytype ob ="+TypeTrans.toMyType(ob)+" type="+TypeTrans.toMyType(ob));
			Args.Add( TypeTrans.toMyType(ob) );
			return this;
		}

		public object Call(IPrintable p) {
			return Proc.Call(p,Args.ToArray());
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is CallFunc) Proc.CompareTo(((CallFunc)ob).Proc);
			return 0;
		}

		public object Clone() {
			return new CallFunc(Proc);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {

		};
		
		static CallFunc() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "CallFunc", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(CallFunc) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return Proc; }
			set { }
		}

		public override string ToString() {
			return "(callfunc: "+Proc+"args: "+Args+")";
		}
	}
}
