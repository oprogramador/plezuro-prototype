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
