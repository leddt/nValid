using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Tests.TestObjects;
using nValid.Validators;

namespace nValid.Tests.Validators
{
    public class RegExValidatorTests : TestBase
    {
        [Test]
        public void Should_be_valid_when_input_matches_pattern()
        {
            var validator = new RegExValidator<Person>("^[A-Z][a-z]{1,4}$");

            Assert.That(validator.Validate(null, "Hello"), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_input_does_not_match_pattern()
        {
            var validator = new RegExValidator<Person>("^[A-Z][a-z]{1,4}$");

            Assert.That(validator.Validate(null, "world"), Is.False);
        }

        [Test]
        public void Has_default_message()
        {
            var validator = new RegExValidator<Person>("^[A-Z][a-z]{1,4}$");

            var message = validator.DefaultErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }

        [Test]
        public void Has_default_negated_message()
        {
            var validator = new RegExValidator<Person>("^[A-Z][a-z]{1,4}$");

            var message = validator.DefaultNegatedErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }
    }
}
