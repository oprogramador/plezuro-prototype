/*
 * ObjectT.cs
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
using MyCollections;
using Engine;
using System.Linq;

namespace MyTypes.MyClasses {
	class ObjectT : IVariable {
		public static readonly string FunctionSymbol = "^^";
		public static readonly string DotSymbol = ".";

		public ObjectT() {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			//int pre = TypeT.PreCompare(this,ob);
			//if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return new ObjectT();
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			"class", 	(Func<IVariable,ClassT>) ((x) => x.GetClass()),
			"print",	(Func<IPrintable,object,object>) ((p,o) => { p.Print( ((IStringable)o).ToString() ); return o; } ),
			"printl",	(Func<IPrintable,object,object>) ((p,o) => { p.PrintLine( ((IStringable)o).ToString() ); return o; } ),
			SymbolMap.ListSymbol,	(Func<ITuplable,IVariable>) ((x) => new ListT(x.ToArray())),
			"lent",		(Func<ITuplable,IVariable>) ((x) => new Number(x.ToArray().Length)),
			"set",		(Func<ITuplable,IVariable>) ((x) => new SetT(x.ToArray())),
			"dic",		(Func<ITuplable,IVariable>) ((x) => new DictionaryT(x.ToArray())),
			DotSymbol,	(Func<IVariable,SoftLink,DotFunc>) ((x,f) => new DotFunc(f.ToProc(x), x)),
			";",		(Func<IVariable,IVariable,IVariable>) ((x,y) => y),
			"=",		(Func<ReferenceT, IVariable, IVariable>) ((x,y) => { x.Value=y; return y;}),
			":=",		(Func<ReferenceT, IVariable, IVariable>) ((x,y) => { 
				Console.WriteLine(":=\ny="+y+" type="+y.GetType()+"clone="+y.Clone()+" type="+y.Clone().GetType());
				x.Value=(IVariable)y.Clone(); return y;}),
			",", 		(Func<ITuplable,ITuplable,ITuplable>) ((a,b) => TupleT.Concat(a,b)),
			":",		(Func<IVariable, IVariable, PairT>) ((x,y) => new PairT(x,y)),
			"<=>",		(Func<IVariable,IVariable,double>) ((x,y) => x.CompareTo(y) ),
			">=",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)>=0 ),
			">",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)>0 ),
			"<=",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)<=0),
			"<",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)<0 ),
			"!=",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)!=0 ),
			"==",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)==0 ),
			"===",		(Func<IVariable,IVariable,bool>) ((x,y) => x==y ),
			"&&",	(Func<ReferenceT,PointerT>) ((x) => new PointerT(x)),
			/*"=",		(Func<ITuplable,ITuplable,ITuplable>) 
					((xx,yy) => { 
						Console.WriteLine("xx="+xx+" type="+xx.GetType());
						Console.WriteLine("yy="+xx+" type="+yy.GetType());
						var x = xx.ToArray();
						var y = yy.ToArray();
						for(int i=0; i<x.Length; i++) {
						Console.WriteLine("i="+i+" x="+x[i]+" type="+x[i].GetType()+" y="+y[i]+" type="+y[i].GetType());
						((ReferenceT)x[i]).Value = y[i];
					}
					return xx;}),
			*/
			};

		private static object[] toRefArray(object[] ar) {
			object[] ret = new object[ar.Length];
			for(int i=0; i<ar.Length; i++) ret[i] = new ReferenceT((IVariable)ar[i]);
			return ret;
		}
	
		static ObjectT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Object", new List<ClassT>(){}, myMethods, PackageT.Lang, typeof(ObjectT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "object";
		}

	}
}
