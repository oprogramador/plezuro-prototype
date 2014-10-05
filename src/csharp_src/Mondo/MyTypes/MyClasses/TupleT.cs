/*
 * TupleT.cs
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
using System.Collections;
using Mondo.MyCollections;
using Mondo.Engine;

namespace Mondo.MyTypes.MyClasses {
	public class TupleT : SList<IVariable>, IVariable {
		public TupleT() :  base() {
			ID = ObjectContainer.Instance.Add(this);
		}

		public TupleT(IEnumerable ie) :  base() {
			ID = ObjectContainer.Instance.Add(this);
			foreach(var i in ie) Add( TypeTrans.toMyType(i) );
		}

		public static ITuplable MakeTuplable(object[] ar) {
			if(ar.Length==0) return new EmptyT();
			else if(ar.Length==1) return TypeTrans.toMyType(ar[0]);
			else return new TupleT(ar);
		}

		public static ITuplable MakeTuplable(object ob) {
			if(ob is object[]) return MakeTuplable((object[])ob);
			return TypeTrans.toMyType(ob);
		}

		public override object Clone() {
			return new TupleT(this);
		}

		public TupleT Concat(TupleT st) {
			return (TupleT)Concat(st,typeof(TupleT));
		}

		public static ITuplable Concat(ITuplable a, ITuplable b) {
			TupleT ret = new TupleT();
			foreach(var i in a.ToArray()) ret.Add(TypeTrans.toRef(i));
			foreach(var i in b.ToArray()) ret.Add(TypeTrans.toRef(i));
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

		private static ClassT myClass;
		
		public const string ClassName = "Tuple";

		private static object[] lambdas = {
			"each",		(Func<IPrintable,TupleT,ProcedureT,object>)
						((p, ar, f) => { object ret=new NullType();
						foreach(IVariable i in ar) ret=Evaluator.Eval(f,p,TupleT.MakeTuplable(i));
						return ret; 
					} ),
		};
		
		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(TupleT) );
			return myClass;
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
