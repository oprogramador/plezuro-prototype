/*
 * ErrorText.cs
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
using Mondo.MyTypes;
using Mondo.MyTypes.MyClasses;

namespace Mondo.Engine {
	class ErrorText {
		private static readonly Dictionary<Type, string> dic = new Dictionary<Type, string> { {typeof(InvalidOperationException), "Error, invalid syntax."}, {typeof(EmptyArgException), "Error, empty set."},
		};

		public Exception Exception { get; private set; }

		public ErrorText(Exception e) {
			Exception = e;
		}

		public override string ToString() {
			try {
				return dic[Exception.GetType()];
			} catch {
				return ""+Exception;
			}
		}
	}
}
