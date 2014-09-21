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
 

using Mondo.MyCollections;
using System;
using System.Collections.Generic;

namespace Mondo.MyTypes.MyClasses {
	class RangeT : IVariable {
		private IStepable beg,end;

		public RangeT(IStepable beg, IStepable end) {
			ID = ObjectContainer.Instance.Add(this);
			this.beg = beg;
			this.end = end;
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = ClassT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is RangeT) return beg.CompareTo(((RangeT)ob).beg);
			return 0;
		}

		public object Clone() {
			return new RangeT(beg,end);
		}

		public override string ToString() {
			return "range";
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;

		public static object[] Constants = {
		};

		private static object[] lambdas = {
		};
		
		static RangeT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Range", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(RangeT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}
	}
}