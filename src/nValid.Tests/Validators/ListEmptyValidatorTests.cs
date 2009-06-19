using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Tests.TestObjects;
using nValid.Validators;

namespace nValid.Tests.Validators
{
    public class ListEmptyValidatorTests : TestBase
    {
        [Test]
        public void Should_be_valid_when_list_empty()
        {
            var validator = new ListEmptyValidator<Person, string>();

            Assert.That(validator.Validate(null, new List<string>()), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_list_not_empty()
        {
            var validator = new ListEmptyValidator<Person, string>();

            Assert.That(validator.Validate(null, new List<string>{"a"}), Is.False);
        }

        [Test]
        public void Has_default_message()
        {
            var validator = new ListEmptyValidator<Person, string>();

            var message = validator.DefaultErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }

        [Test]
        public void Has_default_negated_message()
        {
            var validator = new ListEmptyValidator<Person, string>();

            var message = validator.DefaultNegatedErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }
    }
}
