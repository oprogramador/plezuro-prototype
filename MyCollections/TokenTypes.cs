using System.Windows.Forms;

namespace MyCollections {
	public enum TokenTypes {
		Symbol,
		Number,
		Operator,
		BracketOpen,
		BracketClose,
		SquareOpen,
		SquareClose,
		CurlyOpen,
		CurlyClose,
		OneString,
		BiString,
		TriString,
		SoftLink,
		OneLineComment,
		MultiLineComment,
		WhiteSpace,
		EndLine,
	}

	public static class TokenTypesExtension {
		public static bool IsString(this TokenTypes t) {
			return t==TokenTypes.OneString || t==TokenTypes.BiString || t==TokenTypes.TriString;
		}

		public static bool IsComment(this TokenTypes t) {
			return t==TokenTypes.OneLineComment || t==TokenTypes.MultiLineComment;
		}

		public static bool IsValue(this TokenTypes t) {
			return t==TokenTypes.Symbol || t==TokenTypes.Number || t.IsString() || t==TokenTypes.SoftLink;
		}
	}
}
