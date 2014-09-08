using System;
using System.Collections.Generic;

namespace MyTypes.MyClasses {
	class NullType : IVariable {
		public int ID { get; private set; }

		public NullType() {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return new NullType();
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;

		public static object[] Constants = {
			"null",		new NullType(),
		};

		private static object[] lambdas = {

		};
		
		static NullType() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "NullType", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(NullType) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "null";
		}

	}
}
