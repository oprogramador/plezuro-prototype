/*
 * ProcedureT.cs
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
using System.Diagnostics;

namespace MyTypes.MyClasses {
	public class ProcedureT : OStack, ICallable  {
		public ProcedureT() : base() {
		}

		public ProcedureT(ProcedureT f) :  base(f) {
		}

		public object Call(IPrintable p, object[] args) {
			//foreach(var i in args) Console.WriteLine("i="+i+" type="+i.GetType());
			return p.EvalDyn(this, p, new TupleT(args));
		}

		public override object Clone() {
			return new ProcedureT(this);
		}

		public static new readonly ClassT MyClass;
		public static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			"apply",	(Func<IPrintable,ProcedureT,object>) ((p, f) => p.EvalDyn(f, p)),
			"applyF",	(Func<IPrintable,ProcedureT,ITuplable,object>) ((p, f, a) => p.EvalDyn(f, p, new TupleT(a.ToArray())) ),
			"while",	(Func<IPrintable,ProcedureT,ProcedureT,object>) 
					((p, con, o) => { object ret=new NullType(); 
						while(p.EvalDyn(con,p).Equals(true)) ret=p.EvalDyn(o,p); return ret; } ),
			"time", 	(Func<IPrintable,ProcedureT,double>) 
						((p,f) => {var w=Stopwatch.StartNew(); p.EvalDyn(f,p); return w.ElapsedMilliseconds;}),
		};
		
		static ProcedureT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Procedure", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(ProcedureT) ); 
		}

		public override ClassT GetClass() {
			return MyClass;
		}
	}
}
