/*
 * LambdaConverter.cs
 * Copyright 2014 pierre (Piotr Sroczkowski) <pierre.github@gmail.com>
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301, USA.
 * 
 * 
 */
 

using System;
using System.Collections.Generic;
using MyTypes.MyClasses;
using MyCollections;
using lib;

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
			bool vararg = argt[0]==typeof(ITuplable);// && argnr-(stat?1:0)<2;
			bool tupvar = false;
			if(argt.Length==2) if(argt[0]==typeof(ITuplable) && argt[1]==typeof(IVariable)) tupvar = true;
			//return (Func<ReferenceT>)  (() => new ReferenceT( toMyType( ((Func<ReferenceT>)f)()  ) ));
			var lambda = (Func<IPrintable,object[],ReferenceT>) ((p,argss) => {
				var args = new List<object>(argss);
				Co.Log("fun="+fun,2);
				Co.Log("vararg="+vararg+" stat="+stat,2);
				Co.Log("args="+General.EnumToString(argss),2);
				if(!vararg) for(int i=0; i<args.Count; i++) { 
					Co.Log("i="+i+" argt="+argt[i+(stat?1:0)],2);
					//if(argt[i+(stat?1:0)] != typeof(ITuplable)) {
					//
						args[i] = TypeTrans.dereference(args[i], argt[i+(stat?1:0)]);
						//args[i] = TypeTrans.tryCall(args[i], p, argt[i+(stat?1:0)]);
						args[i] = TypeTrans.adaptType(args[i], argt[i+(stat?1:0)]);
					//}
				}
				else for(int i=0; i<args.Count; i++) {
					args[i] = TypeTrans.toRef( TypeTrans.toMyType( args[i]) );
				}
				Co.Log("#fun="+fun,2);
				Co.Log("#vararg="+vararg+" stat="+stat,2);
				Co.Log("#args="+General.EnumToString(args));
				if(stat) args.Insert(0,p);
				Co.Log("##args="+General.EnumToString(args),2);
				object  res = null;
				int xxx=20;
				xxx++;
				Co.Log("xxx="+xxx,2);
				try {	
					var tup = TupleT.MakeTuplable(args.ToArray());
					Co.Log("tup="+tup,2);
					foreach(var i in args.ToArray()) Co.Log("args: i="+i+" type="+i.GetType(),2);
					Co.Log("args.ToArray="+args.ToArray(),2);
					String strres = ""+res;
					res	= tupvar ? ((Func<ITuplable,IVariable>)fun).Invoke(tup) : 
							fun.DynamicInvoke(args.ToArray());
					Co.Log("xxx="+xxx+" res="+strres+" preres="+res+" type="+res.GetType(),2);
				} catch(Exception e) {
					Co.Log("e:"+e,2); 
					throw new ArgumentException();
				}
				xxx++;
				Co.Log("rrr",2);
				if(res is double) TypeTrans.checkInfNaN(res);
				Co.Log("xxx="+xxx+" res="+res+" type="+res.GetType(),2);
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
