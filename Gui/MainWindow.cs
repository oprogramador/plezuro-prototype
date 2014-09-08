/*
 * MainWindow.cs
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
	class MainWindow : Form {
		private FormAdapter panelAdapter;
		private MainPanel mp;
		private int offset = 20;

		public MainWindow(IOMap map, int w, int h)  {
			new FormAdapter(this).Adapt(0,0,w,h);
			CenterToScreen();
			Panel panel = new Panel();
			panel.AutoScroll = true;
			panelAdapter = new FormAdapter(panel);
			panelAdapter.Adapt(0,0,w-offset,h-offset);
			mp = new MainPanel(map,0,0,w-offset*2,h);
			panel.Controls.Add(mp);
			Controls.Add(panel);
                        Menu = new MyMenu(new string[]{"Help"},
                                new MyItem[][]{
					new MyItem[]{
						new MyItem("Help", new EventHandler(help)),
                                                new MyItem("About", new EventHandler(about)),
					},
                        	});

		}

		private void help(object sender, EventArgs e) {
			MessageBox.Show(Info.Help.Message);
		}

		private void about(object sender, EventArgs e) {
			MessageBox.Show("Author:\t"+Info.Info.Author+"\nYear:\t"+Info.Info.Year+"\nVersion:\t"+Info.Info.Version);
		}

		protected override void OnResize(EventArgs e) {
			if(panelAdapter!=null) panelAdapter.AdaptFill(this,offset);
			if(mp!=null) mp.Width = Width-offset*2;
			Refresh();
		}
	}
}
