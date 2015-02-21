/*
 * WindowT.cs
 * Copyright 2015 pierre (Piotr Sroczkowski) <pierre.github@gmail.com>
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
using System.Windows.Forms;
using System.Drawing;
using System.Timers;
using System.Collections.Generic;
using Mondo.Engine;
using Mondo.MyCollections;

namespace Mondo.MyTypes.MyClasses {
    class WindowT : Form, IVariable {
        private System.Timers.Timer timer;
        public int ID { get; private set; }
        public DictionaryT Dictionary { get; private set; }
        public ProcedureT OnTimeProc { get; private set; }
        public ProcedureT OnKeyPressProc { get; private set; }
        private IPrintable printable;
        private ListT info {
            get {
                return (ListT)((ReferenceT)Dictionary[new ReferenceT(new StringT("info"))]).Value;
            }
            set {}
        }
        private ListT drawingSquares {
            get {
                return (ListT)((PointerT)((ReferenceT)Dictionary[new ReferenceT(new StringT("squares"))]).Value).Value.Value;
            }
            set {}
        }
        private int drawingSquaresWidth {
            get {
                return ((ListT)((ReferenceT)drawingSquares[0]).Value).Count;
            }
            set {}
        }
        public Dictionary<string,Color> Colors = new Dictionary<string,Color>() {
            {"r", Color.Red},
            {"g", Color.Green},
            {"b", Color.Blue},
            {"y", Color.Yellow},
        };

        public WindowT(IPrintable p, DictionaryT dic) {
            ID = ObjectContainer.Instance.Add(this);
            DoubleBuffered = true;
            printable = p;
            Width = (int)((Number)((ReferenceT)dic[new ReferenceT(new StringT("w"))]).Value).Value;
            Height = (int)((Number)((ReferenceT)dic[new ReferenceT(new StringT("h"))]).Value).Value;
            try {
                int tt = (int)((Number)((ReferenceT)dic[new ReferenceT(new StringT("time"))]).Value).Value;
                timer = new System.Timers.Timer(tt);
                timer.Elapsed += onTimeEvent;
                timer.Enabled = true;
            } catch {}
            try {
                OnTimeProc = (ProcedureT)((ReferenceT)dic[new ReferenceT(new StringT("ontime"))]).Value;
            } catch {}
            try {
                OnKeyPressProc = (ProcedureT)((ReferenceT)dic[new ReferenceT(new StringT("keypress"))]).Value;
            } catch {}
            Dictionary = dic;
            KeyPress += new KeyPressEventHandler(onKeyPress);
        }

        private void onKeyPress(object sender, KeyPressEventArgs e) {
            try {
                OnKeyPressProc.Call(printable, new object[]{new StringT(""+e.KeyChar)});
            } catch {}
        }

        private void onTimeEvent(Object source, ElapsedEventArgs e) {
            try {
                OnTimeProc.Call(printable, new object[]{});
            } catch {}
            Refresh();
        }

        public override bool Equals(object ob) {
            return CompareTo(ob)==0;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public int CompareTo(object ob) {
            int pre = ReferenceT.PreCompare(this,ob);
            if(pre!=0) return pre;
            return 0;
        }

        public object Clone() {
            return new WindowT(printable, (DictionaryT)Dictionary.Clone());
        }

        IVariable[] ITuplable.ToArray() {
            return new IVariable[]{};
        }

        private static ClassT myClass;

        public const string ClassName = "Window";

        private static object[] lambdas = {
            "show", 	(Func<WindowT,bool>) ((x) => {x.Show(); Application.Run(x); return true;}),
            "close",	(Func<WindowT,bool>) ((x) => {x.Close(); return true;}),
        };

        public ClassT GetClass() {
            return StaticGetClass();
        }

        public static ClassT StaticGetClass() {
            if(myClass==null) myClass = 
                new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(WindowT) );
            return myClass;
        }	

        public object ObValue {
            get { return this; }
            set { }
        }

        public override string ToString() {
            return "window( "+Dictionary+" )";
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            var g = e.Graphics;
            if(drawingSquares!=null) drawSquares(g);
            if(info!=null) drawInfo(g);
            g.Dispose();
        }

        private void drawInfo(Graphics g) {
            int ww = Width/drawingSquaresWidth;
            int hh = Height/drawingSquares.Count;
            for(int y=0; y<info.Count; y++) {
                var row = (DictionaryT)((ReferenceT)info[y]).Value;
                var size = (int)((Number)((ReferenceT)row[new ReferenceT(new StringT("size"))]).Value).Value;
                var colorstr = (string)((StringT)((ReferenceT)row[new ReferenceT(new StringT("color"))]).Value).Value;
                string text;
                try {
                    text = ((StringT)((PointerT)((ReferenceT)row[new ReferenceT(new StringT("text"))]).Value).Value.Value).Value;
                } catch {
                    text = "";
                }
                var brush = new SolidBrush(Colors[colorstr]);
                var font = new Font("Arial", size);
                g.DrawString(text, font, brush, ww, y*hh, new StringFormat());
                brush.Dispose();
            }
        }

        private void drawSquares(Graphics g) {
            int ww = Width/drawingSquaresWidth;
            int hh = Height/drawingSquares.Count;
            for(int y=0; y<drawingSquares.Count; y++) {
                var row = (ListT)((ReferenceT)drawingSquares[y]).Value;
                for(int x=0; x<row.Count; x++) {
                    var str = ((StringT)((ReferenceT)row[x]).Value).Value;
                    var brush = new SolidBrush(Colors[str]);
                    g.FillRectangle(brush, new Rectangle(x*ww, y*hh, ww, hh));
                    brush.Dispose();
                }
            }
        }
    }
}
