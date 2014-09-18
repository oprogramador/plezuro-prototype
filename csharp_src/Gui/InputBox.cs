/*
 * InputBox.cs
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
using System.Drawing;
using System;
using System.Collections.Generic;
using Engine;
using MyCollections;

namespace Gui {
	class InputBox : IOBox, ITextable {
		private IOMap map;
		private List<Token> myTokens;
		public bool AutoRefresh {
			get;
			set;
		}
		
		public InputBox(IOMap map, int x, int y, int w, int h) : base(x,y,w,h) {
			this.map = map;
			//BackColor = Color.Green;
			KeyUp += new KeyEventHandler(KeyDownFun);
			//VisibleChanged += new EventHandler(ColorEv);
			//KeyDown += new KeyEventHandler(ColorEv);
			KeyPress += new KeyPressEventHandler(ColorEv);
		}
	
		void KeyDownFun(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.F12) Parser.Stop();
			if(AutoRefresh) map.Process(this);
		}

		private void ColorEv(object sender, EventArgs e) {
			if(myTokens!=null) ColorIn(myTokens);
		}

		public void ColorIn(List<Token> tokens) {
			myTokens = tokens;
			int pos = 0;
			int startCursorPosition = SelectionStart;
			int selectionLength = SelectionLength;
			foreach(var t in tokens) {
				if(pos+t.Text.Length >= Text.Length) break;
				Select(pos, pos+t.Text.Length);
				try{
					SelectionColor = VisualSyntax.SyntaxColors[t.Type];
				} catch(Exception e) {
					Console.WriteLine("color e: "+e);
				}
				pos += t.Text.Length;
			}
			SelectionStart = startCursorPosition;
			SelectionLength = selectionLength;
		}

	}
}
