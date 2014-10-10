/*
 * ErrorT.cs
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

namespace Mondo.MyTypes.MyClasses {
	class ErrorT : IVariable {
		private static readonly Dictionary<Type, string> dic = new Dictionary<Type, string> {
			{typeof(OverflowException), "infinity"},
			{typeof(InfinityException), "infinity"},
			{typeof(NaNException), "NaN"},
			{typeof(ModuleNotFoundException), "module not found"},
			{typeof(CircularInheritanceException), "circular inheritance"},
			{typeof(UndefinedException), "undefined"},
			//{typeof(EmptyArgException), "empty set"},
			{typeof(NotComparableException), "invalid object type"},
			{typeof(ArgumentException), "invalid operation"},
			{typeof(IndexOutOfRangeException), "out of range"},
		};

		public Exception Exception { get; private set; }

		public ErrorT(Exception e) {
			ID = ObjectContainer.Instance.Add(this);
			Exception = e;
		}

		public int ID { get; private set; }

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			int pre = ReferenceT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return new ErrorT(Exception);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		private static ClassT myClass;
		
		public const string ClassName = "Error";

		private static object[] lambdas = {
			"msg",		(Func<ErrorT,string>) ((x) => ""+x.Exception),
		};
		
		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(ErrorT) );
			return myClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "Error: "+(dic.ContainsKey(Exception.GetType()) ? dic[Exception.GetType()] : "");
		}
	}
}
