using MyCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using lib;

namespace MyTypes.MyClasses {
	class SoftLink : Pointer<string>, IVariable {	
		public SoftLink(string s) : base(s) {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is string) return Value.CompareTo(ob);
			if(ob is SoftLink) return Value.CompareTo(((SoftLink)ob).Value);
			return 0;
		}

		public object Clone() {
			return new SoftLink((string)Value.Clone());
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public ICallable ToProc(object firstArg) {
			return ((IVariable)firstArg).GetClass().GetMethod(Value).Proc;
		}

		public ICallable ToProc(object[] ar) {
			return ClassT.GetClass(ar).GetMethod(Value).Proc;
		}

		public override string ToString() {
			return "soft link: "+Value;
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			ObjectT.FunctionSymbol,	(Func<IPrintable,SoftLink,ITuplable,object>) ((p,f,a) => {Co.WL("softlambda"); var ar=a.ToArray(); return f.ToProc(ar).Call(p, ar);}),
		};
		
		static SoftLink() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "SoftLink", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(SoftLink) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}
	}
}
