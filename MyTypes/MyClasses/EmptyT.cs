using System;
using System.Collections.Generic;
using Engine;

namespace MyTypes.MyClasses {
	class EmptyT : IVariable {
		public int ID { get; private set; }

		public EmptyT() {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return new EmptyT();
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;

		public static object[] Constants = {
			SymbolMap.EmptySymbol,	new EmptyT(),
		};

		private static object[] lambdas = {
			SymbolMap.ListSymbol,	(Func<EmptyT,IVariable>) ((x) => new ListT()),
		};
		
		static EmptyT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Empty", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(EmptyT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "()";
		}

	}
}
