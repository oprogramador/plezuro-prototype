using MyCollections;
using System;
using System.Collections.Generic;

namespace MyTypes.MyClasses {
	class BooleanT : Pointer<bool>, IVariable {
		public BooleanT(bool x) : base(x) {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is BooleanT) return Value.CompareTo(((BooleanT)ob).Value);
			return 0;
		}

		public object Clone() {
			return new BooleanT(Value);
		}

		public override string ToString() {
			return Value ? "true" : "false";
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;

		public static object[] Constants = {
			"true",		true,
			"false",	false,
		};

		private static object[] lambdas = {
			"if",	(Func<IPrintable,bool,ProcedureT,ProcedureT,object>) ((p, con, t, f) => p.EvalDyn(con ? t : f, p)),
			"?",	(Func<bool,PairT,IVariable>) ((c,p) => c ? p.Key : p.Value),
			"|",	(Func<bool,bool,bool>) ((x,y) => x||y),
			"&",	(Func<bool,bool,bool>) ((x,y) => x&&y),
			"!",	(Func<bool,bool>) ((x) => !x),
		};
		
		static BooleanT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Boolean", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(BooleanT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public override bool Equals(object ob) {
			if(ob is bool) return (bool)ob == Value;
			if(ob is BooleanT) return ((BooleanT)ob).Value == Value;
			return false;
		}

		public override int GetHashCode() {
			return Value ? 1 : 0;
		}
	}
}
