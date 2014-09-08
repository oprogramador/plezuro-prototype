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
