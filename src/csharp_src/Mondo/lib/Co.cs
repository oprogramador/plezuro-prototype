/*
 * Co.cs
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
using System.Diagnostics;
using System.Linq;

namespace Mondo.lib {
	class Co {
		public static void WL(string str) {
			Console.WriteLine(str);
		}

		public static void W(string str) {
			Console.Write(str);
		}

		public static void Log(string str, int n = -1) {
			StackTrace stackTrace = new StackTrace();
			StackFrame[] stackFrames = stackTrace.GetFrames();
			if(n<0) n = stackFrames.Length-1;
			var msg = string.Join(", ",stackFrames.Select((i) => i.GetMethod().DeclaringType+"."+i.GetMethod().Name).ToList().Skip(1).Take(n).ToArray());
			Console.WriteLine("log at ["+msg+"] : "+str+"\n");
		}
	}
}
