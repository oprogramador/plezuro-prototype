using System;
using System.Collections.Generic;
namespace MyTypes.MyClasses {
	class ErrorT : IVariable {
		private static readonly Dictionary<Type, string> dic = new Dictionary<Type, string> {
			{typeof(OverflowException), "infinity"},
			{typeof(InfinityException), "infinity"},
			{typeof(NaNException), "NaN"},
			{typeof(ModuleNotFoundException), "module not found"},
			//{typeof(EmptyArgException), "empty set"},
			{typeof(NotComparableException), "invalid object type"},
			{typeof(ArgumentException), "invalid operation"},
			{typeof(IndexOutOfRangeException), "out of range"},
		};

		public Exception Exception { get; private set; }

		public ErrorT(Exception e) {
			ID = ObjectContainer.Instance.Add(this);
			Exception = e;
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return new ErrorT(Exception);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			"msg",		(Func<ErrorT,string>) ((x) => ""+x.Exception),
		};
		
		static ErrorT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Error", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(ErrorT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "Error: "+(dic.ContainsKey(Exception.GetType()) ? dic[Exception.GetType()] : "")+Exception;
		}
	}
}
