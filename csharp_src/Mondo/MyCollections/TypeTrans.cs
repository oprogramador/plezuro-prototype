/*
 * TypeTrans.cs
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
using System.Collections.Generic;using Mondo.MyTypes;
using Mondo.MyTypes.MyClasses;

namespace Mondo.MyCollections {
	public static class TypeTrans {
		public static object dereference(object ob, Type t) {
			if(ob.GetType() == typeof(ReferenceT)) if(t != typeof(ReferenceT)) return ((ReferenceT)ob).Value;
			return ob;
		}

		public static object dereference(object ob) {
			if(ob.GetType() == typeof(ReferenceT)) return ((ReferenceT)ob).Value;
			return ob;
		}

		public static ReferenceT toRef(IVariable ob) {
			if(ob is ReferenceT) return (ReferenceT)ob;
			return new ReferenceT(ob);
		}

		public static object tryCall(object ob, IPrintable p, Type t) {
			Console.WriteLine("trycall ob="+ob+" type="+ob.GetType()+" t="+t);
			if(ob is DotFunc) if(t != typeof(ICallable)) return ((DotFunc)ob).Call(p, new object[]{});
			return ob;	
		}

		public static object tryCall(object ob, IPrintable p) {
			Console.WriteLine("trycall ob="+ob+" type="+ob.GetType());
			//if(ob is DotFunc) return ((DotFunc)ob).Call(p, new object[]{});
			return ob;	
		}

/*
		public static object fromDic(object ob, object key) {
			if(ob is FuncDictionary) try {
				ob = ((FuncDictionary)ob)[ReferenceT.GetType(toSymbolMapType(key))];
			} catch {
				try {
					ob = ((FuncDictionary)ob)[toMyStrictType(key).GetType()];
				} catch {
					ob = ((FuncDictionary)ob)[typeof(DefaultType)];
				}
			}
			return ob;
		}
*/

		public static object adaptType(object ob, Type t) {
			if(ob is Number) if(t == typeof(double)) return ((Number)ob).Value;
			if(ob is BooleanT) if(t == typeof(bool)) return ((BooleanT)ob).Value;
			if(ob is StringT) if(t == typeof(string)) return ((StringT)ob).Value;
			//if(ob is TypeT) if(t == typeof(Type)) return ((TypeT)ob).Value;
			if(ob is BuiltinFunc) if(t == typeof(Delegate)) return ((BuiltinFunc)ob).Proc;
			//if(ob is PointerT) if(t == typeof(ReferenceT)) return ((PointerT)ob).Value;
			if(ob is double) if(t == typeof(IVariable)) return new Number((double)ob);
			if(ob is bool) if(t == typeof(IVariable)) return new BooleanT((bool)ob);
			if(ob is string) if(t == typeof(IVariable)) return new StringT((string)ob);
			//if(ob is Type) if(t == typeof(IVariable)) return new TypeT((Type)ob);
			//if(ob is Delegate) if(t == typeof(IVariable)) return new BuiltinFunc((Delegate)ob);
			//if(ob is ReferenceT) if(t == typeof(PointerT)) return new PointerT((ReferenceT)ob);
			return ob;	
		}

		public static object toMyStrictType(object ob) {
			if(ob is double) return new Number((double)ob);
			if(ob is bool) return new BooleanT((bool)ob);
			if(ob is string) return new StringT((string)ob);
			//if(ob is Type) return new TypeT((Type)ob);
			//if(ob is Delegate) return new BuiltinFunc((Delegate)ob);
			return ob;
		}

		public static object toSymbolMapType(object ob) {
			if(ob is Number) return ((Number)ob).Value;
			if(ob is BooleanT) return ((BooleanT)ob).Value;
			if(ob is StringT) return ((StringT)ob).Value;
			//if(ob is TypeT) return ((TypeT)ob).Value;
			//if(ob is Delegate) return new BuiltinFunc((Delegate)ob);
			return ob;
		}

		public static IVariable toMyType(object ob) {
			ob = toMyStrictType(ob);
			//if(ob is ReferenceT) return new PointerT((ReferenceT)ob);
			return (IVariable)ob;
		}

		public static void checkInfNaN(object ob) {
			double d = Convert.ToDouble(ob);
			if(Double.IsInfinity(d)) throw new InfinityException();
			if(Double.IsNaN(d)) throw new NaNException();
		}	

	}
}
