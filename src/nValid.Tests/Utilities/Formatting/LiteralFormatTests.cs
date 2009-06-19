// Taken from http://haacked.com/archive/2009/01/14/named-formats-redux.aspx
// Original author : Phil Haack
// Used with permission
// Adapted for nUnit

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Utilities.Formatting;

namespace nValid.Tests.Utilities.Formatting
{
    public class LiteralFormatTests : TestBase
    {
        [Test]
        public void Literal_WithEscapedCloseBraces_CollapsesDoubleBraces()
        {
            //arrange
            var literal = new LiteralFormat("hello}}world");
            //act
            var result = literal.Eval(null);
            //assert
            Assert.That(result, Is.EqualTo("hello}world"));
        }

        [Test]
        public void Literal_WithEscapedOpenBraces_CollapsesDoubleBraces()
        {
            //arrange
            var literal = new LiteralFormat("hello{{world");
            //act
            var result = literal.Eval(null);
            //assert
            Assert.That(result, Is.EqualTo("hello{world"));
        }
    }
}