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
