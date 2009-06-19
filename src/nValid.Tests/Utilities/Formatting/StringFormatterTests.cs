// Taken from http://haacked.com/archive/2009/01/14/named-formats-redux.aspx
// Original author : Phil Haack
// Used with permission
// Adapted for nUnit

using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Utilities.Formatting;

namespace nValid.Tests.Utilities.Formatting
{
    public class StringFormatterTests : TestBase
    {
        private static string Format(string format, object o)
        {
            return HaackFormatter.HaackFormat(format, o);
        }

        [Test]
        public void StringFormat_WithMultipleExpressions_FormatsThemAll()
        {
            //arrange
            var o = new {foo = 123.45, bar = 42, baz = "hello"};

            //act
            var result = Format("{foo} {foo} {bar}{baz}", o);

            //assert
            Assert.That(result, Is.EqualTo("123.45 123.45 42hello"));
        }

        [Test]
        public void StringFormat_WithDoubleEscapedCurlyBraces_DoesNotFormatString()
        {
            //arrange
            var o = new {foo = 123.45};

            //act
            var result = Format("{{{{foo}}}}", o);

            //assert
            Assert.That(result, Is.EqualTo("{{foo}}"));
        }

        [Test]
        public void StringFormat_WithFormatSurroundedByDoubleEscapedBraces_FormatsString()
        {
            //arrange
            var o = new {foo = 123.45};

            //act
            var result = Format("{{{{{foo}}}}}", o);

            //assert
            Assert.That(result, Is.EqualTo("{{123.45}}"));
        }

        [Test]
        public void Format_WithEscapeSequence_EscapesInnerCurlyBraces()
        {
            var o = new {foo = 123.45};

            //act
            var result = Format("{{{foo}}}", o);

            //assert
            Assert.That(result, Is.EqualTo("{123.45}"));
        }

        [Test]
        public void Format_WithEmptyString_ReturnsEmptyString()
        {
            var o = new {foo = 123.45};

            //act
            var result = Format(string.Empty, o);

            //assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Format_WithNoFormats_ReturnsFormatStringAsIs()
        {
            var o = new {foo = 123.45};

            //act
            var result = Format("a b c", o);

            //assert
            Assert.That(result, Is.EqualTo("a b c"));
        }

        [Test]
        public void Format_WithFormatType_ReturnsFormattedExpression()
        {
            var o = new {foo = 123.45};

            //act
            var result = Format("{foo:#.#}", o);

            //assert
            Assert.That(result, Is.EqualTo("123.5"));
        }

        [Test]
        public void Format_WithSubProperty_ReturnsValueOfSubProperty()
        {
            var o = new {foo = new {bar = 123.45}};

            //act
            var result = Format("{foo.bar:#.#}ms", o);

            //assert
            Assert.That(result, Is.EqualTo("123.5ms"));
        }

        [Test, ExpectedException(typeof(FormatException))]
        public void Format_WithFormatNameNotInObject_ThrowsFormatException()
        {
            //arrange
            var o = new {foo = 123.45};

            //act, assert
            Format("{bar}", o);
        }

        [Test, ExpectedException(typeof(FormatException))]
        public void Format_WithNoEndFormatBrace_ThrowsFormatException()
        {
            //arrange
            var o = new {foo = 123.45};

            //act, assert
            Format("{bar", o);
        }

        [Test, ExpectedException(typeof(FormatException))]
        public void Format_WithEscapedEndFormatBrace_ThrowsFormatException()
        {
            //arrange
            var o = new {foo = 123.45};
            
            //act, assert
            Format("{foo}}", o);
        }

        [Test, ExpectedException(typeof(FormatException))]
        public void Format_WithDoubleEscapedEndFormatBrace_ThrowsFormatException()
        {
            //arrange
            var o = new {foo = 123.45};

            //act, assert
            Format("{foo}}}}bar", o);
        }

        [Test, ExpectedException(typeof(FormatException))]
        public void Format_WithDoubleEscapedEndFormatBraceWhichTerminatesString_ThrowsFormatException()
        {
            //arrange
            var o = new {foo = 123.45};

            //act, assert
            Format("{foo}}}}", o);
        }

        [Test]
        public void Format_WithEndBraceFollowedByEscapedEndFormatBraceWhichTerminatesString_FormatsCorrectly()
        {
            var o = new {foo = 123.45};

            //act
            var result = Format("{foo}}}", o);

            //assert
            Assert.That(result, Is.EqualTo("123.45}"));
        }

        [Test]
        public void Format_WithEndBraceFollowedByEscapedEndFormatBrace_FormatsCorrectly()
        {
            var o = new {foo = 123.45};

            //act
            var result = Format("{foo}}}bar", o);

            //assert
            Assert.That(result, Is.EqualTo("123.45}bar"));
        }

        [Test]
        public void Format_WithEndBraceFollowedByDoubleEscapedEndFormatBrace_FormatsCorrectly()
        {
            var o = new {foo = 123.45};

            //act
            var result = Format("{foo}}}}}bar", o);

            //assert
            Assert.That(result, Is.EqualTo("123.45}}bar"));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void Format_WithNullFormatString_ThrowsArgumentNullException()
        {
            //arrange, act, assert
            Format(null, 123);
        }
    }
}