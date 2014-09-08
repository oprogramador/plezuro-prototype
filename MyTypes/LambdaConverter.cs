using System;
using System.Collections.Generic;
using MyTypes.MyClasses;
using MyCollections;

namespace MyTypes {
	static class LambdaConverter {
		public static Dictionary<string,Method> Convert(object[] lambdas) {
			var ret = new Dictionary<string,Method>();
			for(int i=0; i<lambdas.Length; i+=2) ret.Add( (string)lambdas[i], toPublicMethod( toMyLambda((Delegate)lambdas[i+1]) ) );
			return ret;
		}

		private static Method toPublicMethod(BuiltinFunc f) {
			return new Method(f, new AccessModifier(AccessEnum.Public));
		}

		private static BuiltinFunc toMyLambda(Delegate fun) {
			var argt = fun.GetType().GetGenericArguments();
			var argnr = argt.Length-1;
			if(argt[0] == typeof(IPrintable)) argnr--;
			bool stat = argt[0]==typeof(IPrintable);
			bool vararg = argt[0]==typeof(ITuplable) && argnr-(stat?1:0)<2;
			//return (Func<ReferenceT>)  (() => new ReferenceT( toMyType( ((Func<ReferenceT>)f)()  ) ));
			var lambda = (Func<IPrintable,object[],ReferenceT>) ((p,argss) => {
				var args = new List<object>(argss);
				Console.WriteLine("fun="+fun);
				Console.WriteLine("vararg="+vararg+" stat="+stat);
				Console.WriteLine("args="+General.EnumToString(argss));
				if(!vararg) for(int i=0; i<args.Count; i++) { 
					Console.WriteLine("i="+i+" argt="+argt[i+(stat?1:0)]);
					//if(argt[i+(stat?1:0)] != typeof(ITuplable)) {
						args[i] = TypeTrans.dereference(args[i], argt[i+(stat?1:0)]);
						args[i] = TypeTrans.tryCall(args[i], p, argt[i+(stat?1:0)]);
						args[i] = TypeTrans.adaptType(args[i], argt[i+(stat?1:0)]);
					//}
				}
				else for(int i=0; i<args.Count; i++) {
					args[i] = TypeTrans.toRef( TypeTrans.toMyType( TypeTrans.dereference(args[i],typeof(IVariable))) );
				}
				Console.WriteLine("#fun="+fun);
				Console.WriteLine("#vararg="+vararg+" stat="+stat);
				Console.WriteLine("#args="+General.EnumToString(args));
				if(stat) args.Insert(0,p);
				Console.WriteLine("##args="+General.EnumToString(args));
				object  res = null;
				int xxx=20;
				xxx++;
				Console.WriteLine("xxx="+xxx);
				try {	
					var tup = new TupleT(args);
					Console.WriteLine("tup="+tup);
					foreach(var i in args.ToArray()) Console.WriteLine("args: i="+i+" type="+i.GetType());
					Console.WriteLine("args.ToArray="+args.ToArray());
					String strres = ""+res;
					res	= vararg ? ((Func<ITuplable,IVariable>)fun).Invoke(tup) : 
							fun.DynamicInvoke(args.ToArray());
					Console.WriteLine("xxx="+xxx+" res="+strres+" preres="+res+" type="+res.GetType());
				} catch(Exception e) {
					Console.WriteLine("e:"+e); 
					throw new ArgumentException();
				}
				xxx++;
				Console.WriteLine("rrr");
				if(res is double) TypeTrans.checkInfNaN(res);
				Console.WriteLine("xxx="+xxx+" res="+res+" type="+res.GetType());
				return TypeTrans.toRef( toMyType(res) );
			});
			return new BuiltinFunc(lambda, argnr, stat, vararg);
		}

		private static IVariable toMyType(object ob) {
			if(ob is double) return new Number((double)ob);
			if(ob is bool) return new BooleanT((bool)ob);
			if(ob is string) return new StringT((string)ob);
			if(ob is Type) return new TypeT((Type)ob);
			return (IVariable)ob;
		}

		public static object toSimpleType(object ob) {
			if(ob is Number) return ((Number)ob).Value;
			if(ob is BooleanT) return ((BooleanT)ob).Value;
			if(ob is StringT) return ((StringT)ob).Value;
			if(ob is TypeT) return ((TypeT)ob).Value;
			return ob;
		}
	}
}
