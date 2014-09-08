using System;
using System.Collections.Generic;
using MyCollections;
using System.Diagnostics;

namespace MyTypes.MyClasses {
	public class ProcedureT : OStack, ICallable  {
		public ProcedureT() : base() {
		}

		public ProcedureT(ProcedureT f) :  base(f) {
		}

		public object Call(IPrintable p, object[] args) {
			//foreach(var i in args) Console.WriteLine("i="+i+" type="+i.GetType());
			return p.EvalDyn(this, p, new TupleT(args));
		}

		public override object Clone() {
			return new ProcedureT(this);
		}

		public static new readonly ClassT MyClass;
		public static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			"apply",	(Func<IPrintable,ProcedureT,object>) ((p, f) => p.EvalDyn(f, p)),
			"applyF",	(Func<IPrintable,ProcedureT,ITuplable,object>) ((p, f, a) => p.EvalDyn(f, p, new TupleT(a.ToArray())) ),
			"while",	(Func<IPrintable,ProcedureT,ProcedureT,object>) 
					((p, con, o) => { object ret=new NullType(); 
						while(p.EvalDyn(con,p).Equals(true)) ret=p.EvalDyn(o,p); return ret; } ),
			"time", 	(Func<IPrintable,ProcedureT,double>) 
						((p,f) => {var w=Stopwatch.StartNew(); p.EvalDyn(f,p); return w.ElapsedMilliseconds;}),
		};
		
		static ProcedureT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Procedure", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(ProcedureT) ); 
		}

		public override ClassT GetClass() {
			return MyClass;
		}
	}
}
