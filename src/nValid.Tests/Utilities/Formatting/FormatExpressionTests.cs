// Taken from http://haacked.com/archive/2009/01/14/named-formats-redux.aspx
// Original author : Phil Haack
// Used with permission
// Adapted for nUnit

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Utilities.Formatting;

namespace nValid.Tests.Utilities.Formatting
{
    public class FormatExpressionTests : TestBase
    {
        [Test]
        public void Eval_WithNamedExpression_EvalsPropertyOfExpression()
        {
            //arrange
            var expr = new FormatExpression("{foo}");

            //act
            var result = expr.Eval(new {foo = 123});

            //assert
            Assert.That(result, Is.EqualTo("123"));
        }

        [Test]
        public void Eval_WithNamedExpressionAndFormat_EvalsPropertyOfExpression()
        {
            //arrange
            var expr = new FormatExpression("{foo:#.##}");

            //act
            var result = expr.Eval(new {foo = 1.23456});

            //assert
            Assert.That(result, Is.EqualTo("1.23"));
        }

        [Test]
        public void Format_WithColon_ParsesoutFormat()
        {
            //arrange
            var expr = new FormatExpression("{foo:#.##}");

            //assert
            Assert.That(expr.Format, Is.EqualTo("#.##"));
        }

        [Test]
        public void Format_WithExpressionReturningNull_DoesNotThrowException()
        {
            //arrange
            var expr = new FormatExpression("{foo}");

            //assert
            Assert.That(expr.Eval(new {foo = (object) null}), Is.Empty);
        }

        [Test]
        public void Format_WithoutColon_ReadsWholeExpression()
        {
            //arrange
            var expr = new FormatExpression("{foo}");

            //assert
            Assert.That(expr.Expression, Is.EqualTo("foo"));
        }
    }
}