﻿using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Tests.TestObjects;
using nValid.Validators;

namespace nValid.Tests.Validators
{
    public class NullValidatorTests : TestBase
    {
        [Test]
        public void Should_be_valid_when_value_is_null()
        {
            var validator = new NullValidator<Person, string>();

            Assert.That(validator.Validate(null, null), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_value_is_not_null()
        {
            var validator = new NullValidator<Person, string>();
            
            Assert.That(validator.Validate(null, "valid"), Is.False);
        }

        [Test]
        public void Has_default_message()
        {
            var validator = new NullValidator<Person, string>();

            var message = validator.DefaultErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }

        [Test]
        public void Has_default_negated_message()
        {
            var validator = new NullValidator<Person, string>();

            var message = validator.DefaultNegatedErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }
    }
}
