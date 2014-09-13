/*
 * MainPanel.cs
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
	class MainPanel : Panel {
		private IOMap map;
		private List<IOPanel> panels;
		private Button button;
		public int UnitHeight { get; private set; }
		public int PanelNr {
			get {
				return panels.Count;
			}
			set{}
		}
		private int offset = 20;

		public MainPanel(IOMap map, int x, int y, int w, int h) {
			this.map = map;
			UnitHeight = 300;
			panels = new List<IOPanel>();
			new FormAdapter(this).Adapt(x,y,w,h);
			button = new Button();
			AddPanel(null, null);
			Controls.Add( 
				new FormAdapter(button).AdaptClick(new EventHandler(AddPanel), "+++", Color.LawnGreen, 
						0, PanelNr*UnitHeight, Width-offset, UnitHeight/3
					).Control
				);
		}

		public void AddPanel(object sender, EventArgs e) {
			var panel = new IOPanel(this, map, 0, PanelNr*UnitHeight, Width-20, UnitHeight);
			panels.Add(panel);
			Height += UnitHeight;
			Controls.Add(panel);
			button.Top += UnitHeight;
		}

		public void RemovePanel(IOPanel p) {
			if(panels.Count<=0) return;
			int ind = panels.IndexOf(p);
			if(ind<0) return;
			panels.RemoveAt(ind);
			Height -= UnitHeight;
			Controls.Remove(p);
			for(int i=ind; i<panels.Count; i++) panels[i].Top -= UnitHeight;
			button.Top -= UnitHeight;
		}

		protected override void OnResize(EventArgs e) {
			if(panels!=null) foreach(var p in panels) if(p!=null) p.Width = Width-offset;
			if(button!=null) button.Width = Width-offset;
		}
	}
}
