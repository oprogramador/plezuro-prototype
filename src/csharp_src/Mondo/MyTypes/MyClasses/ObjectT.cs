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
using Mondo.MyCollections;
using Mondo.Engine;
using System.Linq;
using Mondo.lib;

namespace Mondo.MyTypes.MyClasses {
	class ObjectT : IVariable {
		public static readonly string FunctionSymbol = "^^";
		public static readonly string DotSymbol = ".";

		public ObjectT() {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int ID { get; private set; }

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			//int pre = ReferenceT.PreCompare(this,ob);
			//if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return new ObjectT();
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		private static ClassT myClass;

		private static object assignLambdaMaker(bool isClone) {
			var cloneF = (Func<object,IVariable>) ((x) => (IVariable)(isClone ? ((IVariable)x).Clone() : x));

			return (Func<ITuplable,ITuplable,ITuplable>) ((xx,yy) => { 
					var x = xx.ToArray();
					if(x.Length<1) return xx;
					var yall = new List<object>(yy.ToArray()).Select((i) => ((IVariable)i).Clone()).ToList();
					if(yall.Count>x.Length) {
					var y = yall.Take(x.Length-1).ToArray();
					var ylast = new TupleT(yall.Skip(x.Length-1));
					for(int i=0; i<x.Length-1; i++) {
					((ReferenceT)x[i]).Value = cloneF(TypeTrans.dereference(y[i]));
					}
					((ReferenceT)x[x.Length-1]).Value = ylast;
					} else {
					var y = yall.ToArray();
					for(int i=0; i<yall.Count; i++) {
					((ReferenceT)x[i]).Value = cloneF(TypeTrans.dereference(y[i]));
					}
					for(int i=yall.Count; i<x.Length; i++) ((ReferenceT)x[i]).Value = new ErrorT(new UndefinedException());
					}
					return xx;
					});
		}

		private static object operMaker(string str) {
			return (Func<IPrintable,ReferenceT,IVariable,IVariable>)
					((p,x,y) => (IVariable)x.GetClass().GetMethod("=").Call(p, new object[]{
						x,
						x.GetClass().GetMethod(str).Call(p, new object[]{x,y})
					}));
		}

		public const string ClassName = "Object";

		private static object[] lambdas = {
			"pools", 	(Func<MyObject,DictionaryT>) ((o) => o.Pools),
			"class", 	(Func<IVariable,ClassT>) ((x) => x.GetClass()),
			"print",	(Func<IPrintable,object,object>) ((p,o) => { p.Print( ((IStringable)o).ToString() ); return o; } ),
			"printl",	(Func<IPrintable,object,object>) ((p,o) => { p.PrintLine( ((IStringable)o).ToString() ); return o; } ),
			SymbolMap.ListSymbol,	(Func<ITuplable,IVariable>) ((x) => new ListT(x.ToArray())),
			"clone",	(Func<IVariable,IVariable>) ((x) => (IVariable)x.Clone()),
			"lent",		(Func<ITuplable,IVariable>) ((x) => new Number(x.ToArray().Length)),
			SymbolMap.SetSymbol,	(Func<ITuplable,IVariable>) ((x) => new SetT(x.ToArray())),
			SymbolMap.DicSymbol,	(Func<ITuplable,IVariable>) ((x) => new DictionaryT(x.ToArray())),
			"toS",		(Func<ITuplable,string>) ((x) => {
						var ret = "";
						foreach(var i in x.ToArray()) ret += ((IStringable)i).ToString();
						return ret;
					}),
			"@",	 	(Func<MyObject,DictionaryT>) ((o) => o.Pools),
			DotSymbol,	(Func<IVariable,SoftLink,DotFunc>) ((x,f) => new DotFunc(f.ToProc(x), x)),
			";",		(Func<IVariable,IVariable,IVariable>) ((x,y) => y),
			//"=",		(Func<ReferenceT, IVariable, IVariable>) ((x,y) => { x.Value=y; return y;}),
			//":=",		(Func<ReferenceT, IVariable, IVariable>) ((x,y) => { 
			//			x.Value=(IVariable)y.Clone();
			//			return y;
			//		}),
			",", 		(Func<ITuplable,ITuplable,ITuplable>) ((a,b) => TupleT.Concat(a,b)),
			"<->",		(Func<ReferenceT,ReferenceT,ReferenceT>) ((a,b) => { 
					var c=a.Value;
					a.Value=b.Value;
					b.Value=c;
					return b; 
					}),
			":",		(Func<IVariable, IVariable, PairT>) ((x,y) => new PairT(x,y)),
			"<=>",		(Func<IVariable,IVariable,double>) ((x,y) => x.CompareTo(y) ),
			">=",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)>=0 ),
			">",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)>0 ),
			"<=",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)<=0),
			"<",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)<0 ),
			"!=",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)!=0 ),
			"==",		(Func<IVariable,IVariable,bool>) ((x,y) => x.CompareTo(y)==0 ),
			"!==",		(Func<IVariable,IVariable,bool>) ((x,y) => x!=y ),
			"===",		(Func<IVariable,IVariable,bool>) ((x,y) => x==y ),
			"&&",	(Func<ReferenceT,PointerT>) ((x) => new PointerT(x)),
			":=",		assignLambdaMaker(true),
			"=",		assignLambdaMaker(false),
			"+=",		operMaker("+"),
			"-=",		operMaker("-"),
			"*=",		operMaker("*"),
			"/=",		operMaker("/"),
			"^=",		operMaker("^"),
			"&=",		operMaker("&"),
			"|=",		operMaker("|"),
			"%=",		operMaker("%"),
			".=",		operMaker("."),
		};

		private static object[] toRefArray(object[] ar) {
			object[] ret = new object[ar.Length];
			for(int i=0; i<ar.Length; i++) ret[i] = new ReferenceT((IVariable)ar[i]);
			return ret;
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(ObjectT) );
			return myClass;
		}

		public ClassT GetClass() {
			return StaticGetClass();
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
