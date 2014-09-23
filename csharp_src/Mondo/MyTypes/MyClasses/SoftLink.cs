/*
 * SoftLink.cs
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
using System.Linq;
using Mondo.lib;

namespace Mondo.MyTypes.MyClasses {
	class SoftLink : Pointer<string>, IVariable {	
		public SoftLink(string s) : base(s) {
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
			int pre = ClassT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is string) return Value.CompareTo(ob);
			if(ob is SoftLink) return Value.CompareTo(((SoftLink)ob).Value);
			return 0;
		}

		public object Clone() {
			return new SoftLink((string)Value.Clone());
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public ICallable ToProc(object firstArg) {
			return ((IVariable)firstArg).GetClass().GetMethod(Value).Proc;
		}

		public ICallable ToProc(object[] ar) {
			return ClassT.GetClass(ar).GetMethod(Value).Proc;
		}

		public override string ToString() {
			return "soft link: "+Value;
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			ObjectT.FunctionSymbol,	(Func<IPrintable,SoftLink,ITuplable,object>) ((p,f,a) => {var ar=a.ToArray(); return f.ToProc(ar).Call(p, ar);}),
		};
		
		static SoftLink() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "SoftLink", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(SoftLink) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}
	}
}
