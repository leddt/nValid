// Taken from http://haacked.com/archive/2009/01/14/named-formats-redux.aspx
// Original author : Phil Haack
// Used with permission

namespace nValid.Utilities.Formatting
{
    internal interface ITextExpression
    {
        string Eval(object o);
    }
}