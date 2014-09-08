using System;
using System.Collections.Generic;

namespace MyTypes.MyClasses {
	class PairT : IVariable {
		public readonly IVariable Key;
		public IVariable Value { get; set; }

		public PairT(IVariable k,IVariable v) {
			ID = ObjectContainer.Instance.Add(this);
			Key = k;
			Value = v;
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is PairT) {
				pre = Key.CompareTo(((PairT)ob).Key);
				if(pre!=0) return pre;
				pre = Value.CompareTo(((PairT)ob).Value);
				return pre;
			}
			return 0;
		}

		public object Clone() {
			return new PairT(Key,Value);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			"key",		(Func<PairT,IVariable>) ((x) => x.Key),
			"value",	(Func<PairT,IVariable>) ((x) => x.Value),
		};
		
		static PairT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Pair", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(PairT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return ""+Key+":"+Value;
		}
	}
}
