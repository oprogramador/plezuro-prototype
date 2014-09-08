using System;
using System.Collections.Generic;

namespace Maths {
	static class NumberCalcul {
		public static readonly double Phi = 0.5*(1+Math.Sqrt(5));

		public static double Fib(double n) {
			return (Math.Pow(Phi,n) - Math.Pow(-Phi,-n))*Math.Pow(5,-0.5);
		}	
	}
}
