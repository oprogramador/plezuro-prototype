/*
 * StaticParser.cs
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
using System.Collections.Generic;using System.Text.RegularExpressions;
using MyTypes;
using MyTypes.MyClasses;

namespace Engine {
	static class StaticParser {
		public static double ParseUniv(string str, long bas) {
			try {
				switch(bas) {
					case 10: return Double.Parse(str, System.Globalization.CultureInfo.InvariantCulture);
					case 16: return ParseInt(str.Substring(2),16);
					case 2: return ParseInt(str.Substring(2),2);
					case 8: return ParseInt(str.Substring(1),8);
				}
			} catch{ throw; }
			return 0;
		}

		public static double ParseDouble(string str) {
			string[] ar = str.Split('E');
			if(ar.Length<2) {
				ar = ar[0].Split('F');
				if(ar.Length<2) return ParseFloat(str);
				return ParseFloat(str)*Math.Pow(10,-ParseInt(ar[1],10));
			}
			return ParseFloat(str)*Math.Pow(10,ParseInt(ar[1],10));
		}

		public static double ParseFloat(string str) {
			long coma=0;
			double x=0;
			foreach(char c in str) {
				if(c=='.') coma--;
				else {
					if(coma>=0) x = x*10+GetDigit(c,10);
					else  {
						x += Math.Pow(10,coma)*GetDigit(c,10);
						coma--;
					}
				}
			}
			return x;
		}

		public static long GetDigit(char c, long bas) {
			long x=0;
			if(Char.IsDigit(c)) x = c-'0';
			if(c>='a' && c<='z') x = c-'a'+10;
			if(x>=bas) x=0;
			return x;
		}

		public static long ParseInt(string str, long bas) {
			long x=0;
			foreach(char c in str) x = x*bas+GetDigit(c,bas);
			if(x<0) throw new InfinityException();
			return x;
		}
	}
}
