using MyCollections;
using System;
using System.Collections.Generic;

namespace MyTypes.MyClasses {
	class PointerT : Pointer<ReferenceT>, IVariable {
		public PointerT(ReferenceT op) :  base(op) {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is PointerT) return Value.Value.ID.CompareTo(((PointerT)ob).Value.Value.ID);
			return Value.Value.CompareTo(((ReferenceT)ob).Value);
		}

		public object Clone() {
			return new PointerT(Value);
		}
		
		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			"**",	(Func<PointerT,ReferenceT>) ((x) => x.Value),
		};
		
		static PointerT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Pointer", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(PointerT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public override string ToString() {
			return "pointer: "+Value.ID;
		}
	}
}
