using MyCollections;
using System;
using System.Collections.Generic;

namespace MyTypes.MyClasses {
	public class ReferenceT : Pointer<IVariable>, IVariable {
		public ReferenceT(IVariable iv) :  base(iv) {
			ID = ObjectContainer.Instance.Add(this);
			Console.WriteLine("reference ctor");
		}

		public static Type GetType(object ob) {
			if(ob is ReferenceT) return ((ReferenceT)ob).Value.GetType();
			return ob.GetType();
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return Value.CompareTo(((ReferenceT)ob).Value);
		}

		public object Clone() {
			return new ReferenceT(Value);
		}
		
		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public IVariable[] RefToArray() {
			return null;
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {

		};
		
		static ReferenceT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Reference", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(ReferenceT) ); 
		}

		public ClassT GetClass() {
			return Value.GetClass();
		}

		string IStringable.ToString() {
			return ""+Value;
		}
	}
}
