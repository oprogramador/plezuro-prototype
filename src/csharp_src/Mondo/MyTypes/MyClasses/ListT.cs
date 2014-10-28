/*
 * ListT.cs
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
 

using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Mondo.MyCollections;
using Mondo.Engine;
using Mondo.lib;

namespace Mondo.MyTypes.MyClasses {
	public class ListT : SList<ICompCloneable>, IVariable, IIndexable {
		public ListT() :  base() {
			ID = ObjectContainer.Instance.Add(this);
		}

		public ListT(IEnumerable ie) :  base(ie) {
			ID = ObjectContainer.Instance.Add(this);
		}

		public override object Clone() {
			return new ListT(this);
		}

		public ListT Concat(ListT st) {
			return (ListT)Concat(st,typeof(ListT));
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		private static ClassT myClass;
		
		public const string ClassName = "List";

		private static object[] lambdas = {
			"toTup",	(Func<ListT,TupleT>) ((a) => new TupleT(a)),
			"get",		(Func<ListT,double,object>) ((a,i) => ((ReferenceT)a[(int)i]).Value ),
			"len",		(Func<ListT,double>) ((a) => a.Count),
			SymbolMap.RefSymbol, (Func<ListT,IVariable,object>) ((a,i) => GeneralIndexer.Index(a,i) ),
			"each",		(Func<IPrintable,ListT,ProcedureT,object>)
						((p, ar, f) => { object ret=new NullType();
						foreach(IVariable i in ar) ret=Evaluator.Eval(f,p,TupleT.MakeTuplable(i));
						return ret; 
					} ),
			"where",	(Func<IPrintable,ListT,ProcedureT,ListT>) 
					((p,x,f) => new ListT( x.Where( i => Evaluator.Eval(f,p,TupleT.MakeTuplable(i)).Equals(true) ).ToList() ) ),
			"map",		(Func<IPrintable,ListT,ProcedureT,ListT>) 
					((p,x,f) => new ListT( x.Select( i => Evaluator.Eval(f,p,TupleT.MakeTuplable(i)) ).ToList() ) ),
			"sort",		(Func<ListT,ListT>) ((x) => {var y=new ListT(x); y.Sort(); return y; } ),
			"orderBy",	(Func<IPrintable,ListT,ProcedureT,ListT>) 
					((p,x,f) => new ListT(x.OrderBy( 
						a => Evaluator.Eval(f,p,TupleT.MakeTuplable(a)) 
					).ToList()) ),
			"orderByD",	(Func<IPrintable,ListT,ProcedureT,ListT>) 
					((p,x,f) => new ListT(x.OrderByDescending( 
						a => Evaluator.Eval(f,p,TupleT.MakeTuplable(a)) 
					).ToList()) ),
			"groupBy",	(Func<IPrintable,ListT,ProcedureT,ListT>)	
					((p,x,f) => new ListT(x.GroupBy(
						a => Evaluator.Eval(f,p,TupleT.MakeTuplable(a))
					).Select(a => new ListT(a.ToList()) ).ToList()) ),
			"reverse",	(Func<ListT,ListT>) ((a) => {var b=new ListT(a); b.Reverse(); return b;}),
			"max",		(Func<ListT,IVariable>) ((x) => (IVariable)((ReferenceT)x.Max()).Value),
			"min",		(Func<ListT,IVariable>) ((x) => (IVariable)((ReferenceT)x.Min()).Value),
			"median",	(Func<ListT,IVariable>) ((x) => (IVariable)((ReferenceT)x.Median()).Value),
			"remove",	(Func<ListT,double,ListT>) ((x,i) => {x.RemoveAt((int)i); return x;}),
			"toSet",	(Func<ListT,IVariable>) ((x) => new SetT(x)),
			"html",		(Func<ListT,string>) ((x) => HtmlTableFactory.Create(x).SetBorder(2).Generate()),
			"<<",		(Func<ListT,IVariable,ListT>) ((a,v) => {a.Add(v); return a;} ),
			">>",		(Func<ListT,ReferenceT,ListT>) ((a,v) => {v.Value=(IVariable)a.Pop(); return a;} ),
			"+",		(Func<ListT,ListT,ListT>) ((x,y) => (ListT)x.Concat(y)),
			"*",		(Func<ListT,double,ListT>) ((x,y) => { 
						var s = new ListT();
						for(int i=0; i<(int)y; i++) s.AddRange((ListT)x.Clone());
						return s;
						}),
		};
		
		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(ListT) );
			return myClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}

		public int ID { get; private set; }

		public override string ToString() {
			string str = "";
			foreach(var item in this) str += (str=="" ? "" : ",") +item;
			return "["+str+"]";
		}

		public object At(int i) {
			return this[i];
		}
	}
}
