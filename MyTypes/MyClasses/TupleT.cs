using System;
using System.Collections.Generic;
using System.Collections;
using MyCollections;

namespace MyTypes.MyClasses {
	public class TupleT : SList<IVariable>, IVariable {
		public TupleT() :  base() {
			ID = ObjectContainer.Instance.Add(this);
		}

		public TupleT(IEnumerable ie) :  base() {
			ID = ObjectContainer.Instance.Add(this);
			foreach(var i in ie) Add( TypeTrans.toMyType(i) );
		}

		public override object Clone() {
			return new TupleT(this);
		}

		public TupleT Concat(TupleT st) {
			return (TupleT)Concat(st,typeof(TupleT));
		}

		public static ITuplable Concat(ITuplable a, ITuplable b) {
			TupleT ret = new TupleT();
			Console.WriteLine("a="+General.EnumToString(a.ToArray()));
			Console.WriteLine("b="+General.EnumToString(b.ToArray()));
			foreach(var i in (a is ReferenceT ? ((ReferenceT)a).Value : a).ToArray()) ret.Add(i);
			foreach(var i in (b is ReferenceT ? ((ReferenceT)b).Value : b).ToArray()) ret.Add(i);
			return ret;
		}

		public static ITuplable Add(ITuplable t, IVariable v) {
			if(t is EmptyT) return v;
			if(t is TupleT) { ((TupleT)t).Add(v); return t; }
			var ret = new TupleT();
			ret.Add(t.ToArray()[0]);
			ret.Add(v);
			return ret;
		}	

		IVariable[] ITuplable.ToArray() {
			return ToArray();
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {

		};
		
		static TupleT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Tuple", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(TupleT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public int ID { get; private set; }

		public override string ToString() {
			string str = "";
			foreach(var item in this) str += (str=="" ? "" : ",") +((IStringable)item).ToString();
			return "("+str+")";
		}
 
	}
}
