using System;
using System.Collections.Generic;

namespace MyTypes.MyClasses {
	class StopPoint : IVariable {
		public int Argnr { get; private set; }

		public StopPoint(int argnr) {
			ID = ObjectContainer.Instance.Add(this);
			Argnr = argnr;
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return new StopPoint(Argnr);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}


		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {

		};
		
		static StopPoint() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "StopPoint", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(StopPoint) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "stop "+Argnr;
		}

	}
}
