using System;
using System.Collections.Generic;

namespace Info {
	class Help {
		public const string Message = 
		@"This is a simple calculator.
		Compiled with mcs 2.10.8.1 (using mono).
		The application provides operations like addition, subtraction, multiplication, division, power, modulo (with '%') and others.
		In future releases there are planned among others: a scripting language, linear algebra, numerical calculations, plots 2D, plots 3D and in general a better GUI.
		This help also is going to be improved.
		Examples of simple operations:
		3+4.45/(45+sin(pi+9))^3
		max(2,4%2,fib(20))
		not trivial functions:
		-median
		-min
		-max
		-fib (fibonacci nth number)
		Moreover it supports other notations:
		0xff1a (hexadecimal)
		0b1101 (binar)
		02351 (octal)
		Happy Easter!!!";
	}
}
