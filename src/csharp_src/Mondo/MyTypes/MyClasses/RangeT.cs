/*
 * RangeT.cs
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
using System.Collections;
using System.Collections.Generic;
using Mondo.MyCollections;
using Mondo.Engine;

namespace Mondo.MyTypes.MyClasses {
	class RangeT : IVariable, IEnumerable {
		public double Beg { get; private set; }
		public double End { get; private set; }
		public double Step { get; private set; }

		public RangeT(double beg, double end, double step) {
			ID = ObjectContainer.Instance.Add(this);
			Beg = beg;
			End = end;
			Step = step;
		}

		public int ID { get; private set; }

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			int pre = ClassT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is RangeT) {
				var obr = (RangeT)ob;
				pre = Beg.CompareTo(obr.Beg);
				if(pre!=0) return pre;
				pre = End.CompareTo(obr.End);
				if(pre!=0) return pre;
				pre = Step.CompareTo(obr.Step);
				if(pre!=0) return pre;
			}
			return 0;
		}

		public object Clone() {
			return new RangeT(Beg, End, Step);
		}

		public override string ToString() {
			return "range("+Beg+", "+End+", "+Step+")";
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public IEnumerator GetEnumerator() {
			for(double x=Beg; x<End; x+=Step) yield return new Number(x);
		}

		private static ClassT myClass;
				public static object[] Constants = {
		};

		public const string ClassName = "Range";

		private static object[] lambdas = {
			"beg",	(Func<RangeT,double>) ((x) => x.Beg),
			"end",	(Func<RangeT,double>) ((x) => x.End),
			"step",	(Func<RangeT,double>) ((x) => x.Step),
			"each", (Func<IPrintable,RangeT,ProcedureT,object>) ((p,r,f) => {
						object ret = new NullType();
						foreach(var x in r) ret=Evaluator.Eval(f,p,TupleT.MakeTuplable(x));
						return ret;
					}),	
		};
		
		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(RangeT) );
			return myClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}
	}
}
