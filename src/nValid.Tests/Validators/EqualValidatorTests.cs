using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Tests.TestObjects;
using nValid.Validators;

namespace nValid.Tests.Validators
{
    public class EqualValidatorTests : TestBase
    {
        [Test]
        public void Should_be_valid_when_values_are_equal()
        {
            var validator = new EqualValidator<Person, int>(15);

            Assert.That(validator.Validate(null, 15), Is.True);
        }

        [Test]
        public void Should_be_valid_when_values_are_equal_using_instance_property()
        {
            var validator = new EqualValidator<Person, int>(p => p.Age);

            var person = new Person {Age = 15};
            Assert.That(validator.Validate(person, 15), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_values_are_not_equal()
        {
            var validator = new EqualValidator<Person, int>(15);

            Assert.That(validator.Validate(null, 51), Is.False);
        }

        [Test]
        public void Should_be_invalid_when_values_are_not_equal_using_instance_property()
        {
            var validator = new EqualValidator<Person, int>(p => p.Age);

            var person = new Person { Age = 15 };
            Assert.That(validator.Validate(person, 51), Is.False);
        }

        [Test]
        public void Has_default_message()
        {
            var validator = new EqualValidator<Person, int>(10);

            var message = validator.DefaultErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }

        [Test]
        public void Has_default_negated_message()
        {
            var validator = new EqualValidator<Person, int>(10);

            var message = validator.DefaultNegatedErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }
    }
}
