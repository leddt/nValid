using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace nValid.Utilities
{
    internal static class Extensions
    {
        public static string GetMemberKey<T1, T2>(this Expression<Func<T1, T2>> expr)
        {
            var memberExpression = GetMemberExpression(expr);

            if (memberExpression == null)
                return expr.Body.ToString();

            return memberExpression.Member.Name;
        }

        public static string GetMemberName<T1, T2>(this Expression<Func<T1, T2>> expr)
        {
            var memberExpression = GetMemberExpression(expr);

            if (memberExpression == null)
                return expr.Body.ToString();

            return memberExpression.Member.Name.PascalSplit();
        }

        public static string PascalSplit(this string input)
        {
            input = Regex.Replace(input, "([a-z](?=[A-Z0-9])|[A-Z0-9](?=[A-Z0-9][a-z]))", "$1 ").Trim();

            return input;
        }

        private static MemberExpression GetMemberExpression<T1,T2>(Expression<Func<T1, T2>> expr)
        {
            return expr.Body as MemberExpression;
        }
    }
}
