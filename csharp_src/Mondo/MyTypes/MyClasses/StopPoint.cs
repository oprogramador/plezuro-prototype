/*
 * StopPoint.cs
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
	class StopPoint : IVariable {
		public int Argnr { get; private set; }

		public StopPoint(int argnr) {
			ID = ObjectContainer.Instance.Add(this);
			Argnr = argnr;
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = ClassT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return new StopPoint(Argnr);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static ClassT MyClass;

		private static object[] lambdas = {

		};
		
		public virtual ClassT GetClass() {
			if(MyClass==null) MyClass = 
				new BuiltinClass( "StopPoint", 
						new List<ClassT>(){ObjectT.StaticGetClass()},
						LambdaConverter.Convert(lambdas),
						PackageT.Lang,
						typeof(StopPoint) );
			return MyClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "stop "+Argnr;
		}

	}
}
