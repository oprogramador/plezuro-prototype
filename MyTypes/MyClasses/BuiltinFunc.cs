using System;
using System.Collections.Generic;
using MyCollections;

namespace MyTypes.MyClasses {
	class BuiltinFunc : IVariable, ICallable  {
		public Func<IPrintable,object[],ReferenceT> Proc { get; private set; }

		public BuiltinFunc(Func<IPrintable,object[],ReferenceT> p, int argnr, bool stat, bool vararg) {
			ID = ObjectContainer.Instance.Add(this);
			Proc = p;
			Argnr = argnr;
			IsStatic = stat;
			IsVarArg = vararg;
		}

		public int Argnr { get; private set; }
		public bool IsStatic { get; private set; }
		public bool IsVarArg { get; private set; }

		public object Call(IPrintable p, object[] argss) {
			/*
			var args = new List<object>(argss);
			var argt = Proc.GetType().GetGenericArguments();
			var argnr = argt.Length-1;
			if(argt[0] == typeof(IPrintable)) argnr--;
			bool stat = argt[0]==typeof(IPrintable);
			bool vararg = argt[0]==typeof(ITuplable) && argnr-(stat?1:0)<2;
			if(vararg) argnr = args.Count;
			if(!vararg) for(int i=0; i<argnr; i++) {
				args[i] = TypeTrans.dereference(args[i], argt[i+(stat?1:0)]);
				args[i] = TypeTrans.tryCall(args[i], p, argt[i+(stat?1:0)]);
				args[i] = TypeTrans.adaptType(args[i], argt[i+(stat?1:0)]);
			}
			else for(int i=0; i<argnr; i++) {
				args[i] = new ReferenceT( TypeTrans.toMyType( TypeTrans.dereference(args[i],typeof(IVariable))));
			}
			//if(argnr>0) if(args[0] is FuncDictionary) args[0] = new BuiltinFunc((Delegate)TypeTrans.fromDic(args[0],((ITuplable)args[1]).ToArray()[0]));
			if(stat) args.Insert(0,p);
			object  res = null;
		       	try {	
				var tup = new TupleT(args);
				res	= vararg ? ((Func<ITuplable,IVariable>)Proc).Invoke(tup) : 
						Proc.DynamicInvoke(args.ToArray());
			} catch { 
				throw new ArgumentException();
			}
			if(res is double) TypeTrans.checkInfNaN(res);
			return res;
			*/
			Console.WriteLine("call");
			return Proc(p,argss);
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return new BuiltinFunc(Proc, Argnr, IsStatic, IsVarArg);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {

		};
		
		static BuiltinFunc() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "BuiltinFunc", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(BuiltinFunc) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "builtin: "+Proc;
		}
	}
}
