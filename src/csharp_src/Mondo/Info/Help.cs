/*
 * Help.cs
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

namespace Mondo.Info {
	class Help {
		public const string Message = 
		@"This is a simple calculator.
		Compiled with mcs 2.10.8.1 (using Mondo.mono).
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
