/*
 * Number.cs
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
 

using Mondo.MyCollections;
using System;
using System.Collections.Generic;
using Mondo.Maths;

namespace Mondo.MyTypes.MyClasses {
	class Number : Pointer<double>, IVariable {
		public Number(double x) : base(x) {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = ClassT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is Number) return Value.CompareTo(((Number)ob).Value);
			return -1;
		}

		public object Clone() {
			return new Number(Value);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static ClassT MyClass;

		public static object[] Constants = {
			"pi",		Math.PI,
			"e",		Math.E,
		};

		private static object[] lambdas = {
			"chr",		(Func<double,string>) ((x) => ""+(char)x),
			"sin",		(Func<double,double>) ((x) => {Console.WriteLine("num sin"); return Math.Sin(x);}),
			"cos",		(Func<double,double>) ((x) => Math.Cos(x)),
			"tan",		(Func<double,double>) ((x) => Math.Tan(x)),
			"asin",		(Func<double,double>) ((x) => Math.Asin(x)),
			"acos",		(Func<double,double>) ((x) => Math.Acos(x)),
			"atan",		(Func<double,double>) ((x) => Math.Atan(x)),
			"sinh",		(Func<double,double>) ((x) => Math.Sinh(x)),
			"cosh",		(Func<double,double>) ((x) => Math.Cosh(x)),
			"tanh",		(Func<double,double>) ((x) => Math.Tanh(x)),
			"round",	(Func<double,double>) ((x) => Math.Round(x)),
			"floor",	(Func<double,double>) ((x) => Math.Floor(x)),
			"ceil",		(Func<double,double>) ((x) => Math.Ceiling(x)),
			"abs",		(Func<double,double>) ((x) => Math.Abs(x)),
			"ln",		(Func<double,double>) ((x) => Math.Log(x)),
			"log",		(Func<double,double>) ((x) => Math.Log10(x)),
			"sqrt",		(Func<double,double>) ((x) => Math.Sqrt(x)),
			"fib",		(Func<double,double>) ((x) => NumberCalcul.Fib(x)),
			"+",		(Func<double,double,double>) ((x,y) => x+y),
			"-",		(Func<double,double,double>) ((x,y) => x-y),
			"%",		(Func<double,double,double>) ((x,y) => (int)x%(int)y),
			"*",		(Func<double,double,double>) ((x,y) => x*y),
			"/",		(Func<double,double,double>) ((x,y) => x/y),
			"^",		(Func<double,double,double>) ((x,y) => Math.Pow(x,y)),
			"++",		(Func<ReferenceT, object>) ((x) => { ((Number)x.Value).Value++; return x; }),
			"--",		(Func<ReferenceT, object>) ((x) => { ((Number)x.Value).Value--; return x; }),
		};

		private object convertLambda(object f) {
			return (Func<Number,Number,Number>) ((x,y) => new Number(((Func<double,double,double>)f)(x.Value, y.Value)));
		}

		public virtual ClassT GetClass() {
			if(MyClass==null) MyClass = 
				new BuiltinClass( "Number", 
						new List<ClassT>(){ObjectT.StaticGetClass()},
						LambdaConverter.Convert(lambdas),
						PackageT.Lang,
						typeof(Number) );
			return MyClass;
		}	

		public override string ToString() {
			return ""+Value;
		}
	}
}
