/*
 * IOPanel.cs
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
using System.Collections.Generic;using Engine;

namespace Gui {
	class IOPanel : Panel {
		MainPanel mp;
		IOMap map;
		private InputBox inb;
		private OutputBox outb;
		private Panel controlPanel;
		private int panWid = 50;
		private int borThick = 12;
		private Brush borderBrush = Brushes.Maroon;

		public IOPanel(MainPanel mp, IOMap map, int x, int y, int w, int h) {
			this.mp = mp;
			this.map = map;
			new FormAdapter(this).Adapt(x,y,w,h);
			inb  = new InputBox(map, borThick, borThick, w-panWid-borThick*2, h/2-borThick);
			outb = new OutputBox(borThick, h/2, w-panWid-borThick*2, h/2);
			map.Add(inb,outb);
			Controls.Add(inb);
			Controls.Add(outb);
			controlPanel = new Panel();
			new FormAdapter(controlPanel).Adapt(Width-panWid-borThick, borThick, panWid, h-borThick*2);
			controlPanel.BackColor = Color.MediumBlue;
			controlPanel.Controls.Add( 
				new FormAdapter(new Button()).AdaptClick( new EventHandler(removeSelf), "x", Color.Red, 0, 0, panWid, panWid).Control
				);
			controlPanel.Controls.Add(
				new FormAdapter(new Button()).AdaptClick( new EventHandler(submit), "Sumbit", Color.LawnGreen, 0, panWid*2, panWid, panWid).Control
				);
			controlPanel.Controls.Add(
				new FormAdapter(new CheckBox()).AdaptClick( new EventHandler(switchAuto), "Auto", Color.PaleGreen, 0, panWid, panWid, panWid).Control
				);
			Controls.Add(controlPanel);
		}

		public void removeSelf(object sender, EventArgs e) {
			mp.RemovePanel(this);
		}

		public void submit(object sender, EventArgs e) {
			map.Process(inb);
		}

		public void switchAuto(object sender, EventArgs e) {
			inb.AutoRefresh = !inb.AutoRefresh;
		}

		protected override void OnResize(EventArgs e) {
			if(inb!=null) inb.Width = Width-panWid-borThick*2;
			if(outb!=null) outb.Width = Width-panWid-borThick*2;
			if(controlPanel!=null) controlPanel.Left = Width-panWid-borThick;
		}

		protected override void OnPaint(PaintEventArgs e) {
			Pen pen = new Pen(borderBrush);
			pen.Width = borThick;
			e.Graphics.DrawRectangle(pen, new Rectangle(0,0,Width,Height));
		}
	}
}
