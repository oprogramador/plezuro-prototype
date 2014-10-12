/*
 * StringT.cs
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
 

using Mondo.MyCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using Mondo.Engine;

namespace Mondo.MyTypes.MyClasses {
	class StringT : Pointer<string>, IVariable {	
		public static readonly Dictionary<string,char> escapeChars = new Dictionary<string,char> {
			{"\\\\",	'\\'},
			{"\\\'",	'\''},
			{"\\\"",	'\"'},
			{"\\t",		'\t'},
			{"\\b",		'\b'},
			{"\\n",		'\n'},
			{"\\f",		'\f'}, 
			{"\\v",		'\v'}, 
			{"\\r",		'\r'}, 
			{"\\0",		'\0'},
		};

		public static Dictionary<char,string> revEscape { get; private set; }

		public StringT() : base("") {
			ID = ObjectContainer.Instance.Add(this);
		}

		public StringT(string s) : base(s) {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int ID { get; private set; }

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			int pre = ReferenceT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is string) return Value.CompareTo(ob);
			if(ob is StringT) return Value.CompareTo(((StringT)ob).Value);
			return 0;
		}

		public object Clone() {
			return new StringT((string)Value.Clone());
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public override string ToString() {
			string ret =  "\"";
			foreach(var i in Value) ret += revEscape.ContainsKey(i) ? revEscape[i] : ""+i;
			ret += "\"";
			return ret;
		}

		string IStringable.ToString() {
			return Value;
		}

		private static ClassT myClass;
				public const string ClassName = "String";

		private static object[] lambdas = {
			"newClass",	(Func<string,ListT,DictionaryT,ClassT>) 
					((name, parents, methods) => new MyClass(name, parents, methods)),
			"len",		(Func<string,double>) ((a) => a.Length),
			"get",		(Func<string,double,IVariable>) ((a,i) => new StringT(""+a[(int)i])),
			"reverse",	(Func<string,string>) ((a) => {var c=a.ToCharArray(); Array.Reverse(c); return new string(c);}),
			"ord",		(Func<string,ITuplable>) ((x) => TupleT.MakeTuplable(new ListT((from ch in x select (double)ch).ToList()).ToArray())),
			"fromF",	(Func<string,object>) ((x) => { try{ return System.IO.File.ReadAllText(x); }catch{ return new NullType(); }}),
			"toF",		(Func<string,string,bool>) ((x,f) => { try{ System.IO.File.WriteAllText(f,x); return true; }catch{ return false; }}),
			"put",		(Func<string,string,bool>) ((f,x) => { try{ System.IO.File.WriteAllText(f,x); return true; }catch{ return false; }}),
			"putA",		(Func<string,string,bool>) ((f,x) => { try{ System.IO.File.AppendAllText(f,x); return true; }catch{ return false; }}),
			"append",	(Func<string,string,bool>) ((x,f) => { try{ System.IO.File.AppendAllText(f,x); return true; }catch{ return false; }}),
			"load",		(Func<IPrintable,string,object>) 
					((p,x) => { try{ return Parser.Parse(System.IO.File.ReadAllText(x).TrimEnd('\n'), p, null); } 
						catch{ throw new ModuleNotFoundException(); }
					}),
			"eval",		(Func<IPrintable,string,object>) ((p,code) => Parser.Parse(code, p, null) ),
			"+", 		(Func<IPrintable,string,IVariable,string>) ((p,x,y) => {
						try {
							var f = y.GetClass().GetMethod("str");
							return x+((IStringable)f.Call(p, new object[]{y})).ToString();
						} catch {}
						return x+((IStringable)y).ToString();
					}),
			"*",		(Func<string,double,string>) ((x,y) => { 
						var s = new StringBuilder();
						for(int i=0; i<(int)y; i++) s.Append(x);
						return ""+s;
						}),
			"#",	(Func<IPrintable,StringT,object>) ((p,t) => t.Hash(p) ),
			"=~",	(Func<string,string,bool>) ((r,t) => new Regex(r).IsMatch(t)),
			"!~",	(Func<string,string,bool>) ((r,t) => !new Regex(r).IsMatch(t)),
		};
		
		static StringT() {
			revEscape = escapeChars.ToDictionary(x => x.Value, x => x.Key);
		}

		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(StringT) );
			return myClass;
		}	

		class MiniParser : IParseable {
			public object Parse(string str, ITextable t) {
				return Parser.Parse(str,t);
			}
		}

		private object toHash(ref int i, IPrintable p) {
			string code = "";
			int bra = 1;
			for(i+=2; i<Value.Length; i++) {
				if(Value[i]=='{') bra++;
				if(Value[i]=='}') bra--;
				if(bra==0) break;
				code += Value[i];
			}
			return Parser.Parse(code,p,null);
		}

		public string Hash(IPrintable p) {
			string ret = "";
			for(int i=0; i<Value.Length; i++) {
				if(Value[i]=='#') if(i+1<Value.Length) if(Value[i+1]=='{') {
					ret += toHash(ref i, p);
					continue;
				}
				ret += Value[i];
			}
			return ret;
		}
	}
}
