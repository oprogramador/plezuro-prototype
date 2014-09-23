/*
 * Integral.cs
 * Copyright 2014 Daniel Mikulski
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

namespace Mondo.lib {
	class Integral {
		public double Result { get; private set; }

		public Integral(Func<double,double> f, double beg, double end, int n) {
			double dx,s = 0;
			dx= (end-beg)/n;
			for( double x = beg; x < end; x+=dx ) 
				s += f( x );
			s *= dx;
			Result = s;
		}
	}
}
