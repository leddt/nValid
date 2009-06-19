// Taken from http://haacked.com/archive/2009/01/14/named-formats-redux.aspx
// Original author : Phil Haack
// Used with permission

using System;
using System.Web;
using System.Web.UI;

namespace nValid.Utilities.Formatting
{
    internal class FormatExpression : ITextExpression
    {
        private readonly bool _invalidExpression;

        public string Expression { get; private set; }
        public string Format { get; private set; }

        public FormatExpression(string expression)
        {
            if (!expression.StartsWith("{") || !expression.EndsWith("}"))
            {
                _invalidExpression = true;
                Expression = expression;
                return;
            }

            var expressionWithoutBraces = expression.Substring(1, expression.Length - 2);
            var colonIndex = expressionWithoutBraces.IndexOf(':');

            if (colonIndex < 0)
                Expression = expressionWithoutBraces;
            else
            {
                Expression = expressionWithoutBraces.Substring(0, colonIndex);
                Format = expressionWithoutBraces.Substring(colonIndex + 1);
            }
        }

        public string Eval(object o)
        {
            if (_invalidExpression)
                throw new FormatException("Invalid expression");

            try
            {
                if (String.IsNullOrEmpty(Format))
                    return (DataBinder.Eval(o, Expression) ?? string.Empty).ToString();

                return (DataBinder.Eval(o, Expression, "{0:" + Format + "}") ?? string.Empty);
            }
            catch (HttpException)
            {
                throw new FormatException();
            }
        }
    }
}