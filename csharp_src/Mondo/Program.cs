/*
 * Program.cs
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
 

using System.Windows.Forms;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Mondo.Gui;

namespace Mondo.Program {
	class Program {
		private static void startGui() {
			Application.Run(new MainWindow(Engine.Engine.GetInstance().IOMap, 600, 400));
		}

		private static void startConsole(string[] args) {
			var l = new List<object>(args).Skip(1).ToArray();
			Console.WriteLine( Engine.Parser.Parse(System.IO.File.ReadAllText(args[0]).TrimEnd('\n'), MyTypes.MyClasses.TupleT.MakeTuplable(l)) );
		}

		public static void Main(string[] args) {
			try {
				Application.ThreadException += new ThreadExceptionEventHandler(excHandle);
				System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
				Engine.Engine.GetInstance();
				new Tests.TestUnit();
				if(args.Length>0) startConsole(args);
				else startGui();
			} catch(Exception e) {
				System.Console.WriteLine("e: "+e);
			}
		}

		private static void excHandle(object sender, ThreadExceptionEventArgs e) {
			Console.WriteLine("e: "+e);
		}
	}
}
