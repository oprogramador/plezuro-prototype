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
using Mondo.MyTypes.MyClasses;
using Mondo.MyCollections;
using Mondo.lib;

namespace Mondo.MyTypes {
	static class LambdaConverter {
		public static Dictionary<string,Method> Convert(object[] lambdas) {
			var ret = new Dictionary<string,Method>();
			for(int i=0; i<lambdas.Length; i+=2) {
				ret.Add( (string)lambdas[i], toPublicMethod( toMyLambda((Delegate)lambdas[i+1]) ) );
			}
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
			bool vararg = argt[stat?1:0]==typeof(ITuplable);// && argnr-(stat?1:0)<2;
			bool tupvar = false;
			if(argt.Length==2) if(argt[0]==typeof(ITuplable) && argt[1]==typeof(IVariable)) tupvar = true;
			//return (Func<ReferenceT>)  (() => new ReferenceT( toMyType( ((Func<ReferenceT>)f)()  ) ));
			var lambda = (Func<IPrintable,object[],ReferenceT>) ((p,argss) => {
				var args = new List<object>(argss);
				//Console.WriteLine("\n\nfun="+fun);
				//Console.WriteLine("lambda vararg="+vararg+" stat="+stat+" tupvar="+tupvar);
				//Console.WriteLine("args="+General.EnumToString(args));
				if(!vararg) for(int i=0; i<args.Count; i++) { 
					//if(argt[i+(stat?1:0)] != typeof(ITuplable)) {
					//
						args[i] = TypeTrans.dereference(args[i], argt[i+(stat?1:0)]);
						args[i] = TypeTrans.tryCall(args[i], p, argt[i+(stat?1:0)]);
						args[i] = TypeTrans.adaptType(args[i], argt[i+(stat?1:0)]);
					//}
				}
				else for(int i=0; i<args.Count; i++) {
					args[i] = TypeTrans.toRef( TypeTrans.toMyType( args[i]) );
				}
                                if(!tupvar) for(int i=args.Count; i<argnr; i++) args.Add(null);
				if(stat) args.Insert(0,p);
				//Console.WriteLine("args="+General.EnumToString(args));
				//Console.WriteLine("args.to_a="+General.EnumToString(args.ToArray()));
				object  res = null;
				try {	
					var tup = TupleT.MakeTuplable(args.ToArray());
					//Console.WriteLine("tup="+tup);
					res	= tupvar ? ((Func<ITuplable,IVariable>)fun).Invoke(tup) : 
							fun.DynamicInvoke(args.ToArray());
					//Console.WriteLine("res="+res);
					//Console.WriteLine("args="+General.EnumToString(args));
				} catch {
					//Console.WriteLine("e="+e);
					throw new ArgumentException();
				}
				if(res is double) TypeTrans.checkInfNaN(res);
				return TypeTrans.toRef( toMyType(res) );
			});
			return new BuiltinFunc(lambda, argnr, stat, vararg);
		}

		private static IVariable toMyType(object ob) {
			if(ob is double) return new Number((double)ob);
			if(ob is bool) return new BooleanT((bool)ob);
			if(ob is string) return new StringT((string)ob);
			//if(ob is Type) return new TypeT((Type)ob);
			return (IVariable)ob;
		}
	}
}
