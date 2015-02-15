/*
 * FormAdapter.cs
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
using System.Windows.Forms;
using System.Drawing;

namespace Mondo.Gui {
	public class FormAdapter {
		public Control Control { get; private set; }

		public FormAdapter(Control c) {
			Control = c;
		}

		public FormAdapter Adapt(int x,int y,int w,int h) {
			Control.Width = w;
			Control.Height = h;
			Control.Left = x;
			Control.Top = y;
			return this;
		}

		public FormAdapter Adapt(string text, int x, int y, int w, int h) {
			Adapt(x,y,w,h);
			Control.Text = text;
			return this;
		}
		
		public FormAdapter Adapt(string text, Color fc, Color bc, int x, int y, int w, int h) {
			Adapt(x,y,w,h);
			SetColors(fc,bc);
			Control.Text = text;
			return this;
		}
		
		public FormAdapter Adapt(string text, Color bc, int x, int y, int w, int h) {
			Adapt(x,y,w,h);
			Control.BackColor = bc;
			Control.Text = text;
			return this;
		}

		public FormAdapter AdaptConst(int x, int y, int w, int h) {
			Adapt(x,y,w,h);
			Control.MinimumSize = new Size(w,h);
			Control.MaximumSize = new Size(w,h);
			return this;
		}	

		public FormAdapter AdaptClick(IClickable cli, string name, Color backCol, int x, int y, int w, int h) {
			Adapt(name,backCol,x,y,w,h);
			Control.Click += new EventHandler(cli.ClickFun);
			return this;
		}
		
		public FormAdapter AdaptClick(EventHandler eh, string name, Color backCol, int x, int y, int w, int h) {
			Adapt(name,backCol,x,y,w,h);
			Control.Click += eh;
			return this;
		}

		public FormAdapter AdaptLike(Control c) {
			Control.Width = c.Width;
			Control.Height = c.Height;
			Control.Left = c.Left;
			Control.Top = c.Top;
			Control.MinimumSize = c.MinimumSize;
			Control.MaximumSize = c.MaximumSize;
			return this;
		}

		public FormAdapter AdaptFill(Control c) {
			Control.Width = c.Width;
			Control.Height = c.Height;
			return this;
		}
		
		public FormAdapter AdaptFill(Control c, int offset) {
			Control.Width = c.Width-offset;
			Control.Height = c.Height-offset;
			return this;
		}

		public FormAdapter Adapt(int x,int y) {
			Control.Left = x;
			Control.Top = y;
			return this;
		}

		public FormAdapter SetColors(Color fc, Color bc) {
			Control.ForeColor = fc;
			Control.BackColor = bc;
			return this;
		}

	}

}
