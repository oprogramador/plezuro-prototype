/*
 * IOMap.cs
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
using Mondo.Controller;

namespace Mondo.Engine {
	class IOMap : Dictionary<ITextable, IOutputable>, IRefreshable {
		private Parser par;
		private ITextable t;

		public IOMap() {

		}

		public void Process(ITextable t) {
			this.t = t;
			this[t].Text = "";
			par = new Parser(this, t.Text, t, this[t], null);
		}

		public void Refresh() {
			try {
                            this[t].Text += par.Str;
                            this[t].ShowError(!par.IsCorrectSyntax);
			} catch(Exception e) {
				System.Console.WriteLine("IOMap e: "+e);
			}
		}
	}
}
