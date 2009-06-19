// Taken from http://haacked.com/archive/2009/01/14/named-formats-redux.aspx
// Original author : Phil Haack
// Used with permission

namespace nValid.Utilities.Formatting
{
    internal class LiteralFormat : ITextExpression
    {
        public string LiteralText { get; private set; }

        public LiteralFormat(string literalText)
        {
            LiteralText = literalText;
        }

        public string Eval(object o)
        {
            var literalText = LiteralText
                .Replace("{{", "{")
                .Replace("}}", "}");

            return literalText;
        }
    }
}