using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Tests.TestObjects;
using nValid.Validators;

namespace nValid.Tests.Validators
{
    public class CustomValidatorTests : TestBase
    {
        [Test]
        public void Should_be_valid_when_condition_returns_true()
        {
            var validator = new CustomValidator<Person, int>(v => v%2 == 0);

            Assert.That(validator.Validate(null, 16), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_condition_returns_false()
        {
            var validator = new CustomValidator<Person, int>(v => v % 2 == 0);

            Assert.That(validator.Validate(null, 21), Is.False);
        }

        [Test]
        public void Should_be_valid_when_condition_returns_true_using_instance_property()
        {
            var validator = new CustomValidator<Person, int>((p, v) => p.Age % v == 0);

            var person = new Person {Age = 16};
            Assert.That(validator.Validate(person, 4), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_condition_returns_false_using_instance_property()
        {
            var validator = new CustomValidator<Person, int>((p, v) => p.Age % v == 0);

            var person = new Person { Age = 16 };
            Assert.That(validator.Validate(person, 3), Is.False);
        }

        [Test]
        public void Has_default_message()
        {
            var validator = new CustomValidator<Person, int>(v => v%2 == 0);
            
            var message = validator.DefaultErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }

        [Test]
        public void Has_default_negated_message()
        {
            var validator = new CustomValidator<Person, int>(v => v % 2 == 0);

            var message = validator.DefaultNegatedErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }
    }
}
