/*
 * General.cs
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
using System.Text;
using System;
using System.Collections.Generic;
namespace MyCollections {
	static class General {
		public static string EnumToString(IEnumerable ie) {
			string str = "";
			foreach(var item in ie) str += (str=="" ? "" : ",") +item;
			return "["+str+"]";
		}

		public static bool Converges(string str, string ch, int index) {
			for(int i=index; i<str.Length && i-index<ch.Length; i++) if(str[i] != ch[i-index]) return false;
			return true;
		}

		public static bool Converges(StringBuilder str, string ch, int index) {
			for(int i=index; i<str.Length && i-index<ch.Length; i++) if(str[i] != ch[i-index]) return false;
			return true;
		}

		public static object[] Shift(object[] ar, object x) {
			object[] ret = new object[ar.Length+1];
			ret[0] = x;
			for(int i=0; i<ar.Length; i++) ret[i+1] = ar[i];
			return ret;
		}
	}
}
