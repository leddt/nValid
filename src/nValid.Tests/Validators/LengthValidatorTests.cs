using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Tests.TestObjects;
using nValid.Validators;

namespace nValid.Tests.Validators
{
    public class LengthValidatorTests : TestBase
    {
        [Test]
        public void Should_be_valid_when_length_is_between_values()
        {
            var validator = new LengthValidator<Person>(2, 5);

            Assert.That(validator.Validate(null, "ABC"), Is.True);
        }

        [Test]
        public void Should_be_valid_when_length_is_equal_to_min_value()
        {
            var validator = new LengthValidator<Person>(2, 5);

            Assert.That(validator.Validate(null, "AB"), Is.True);
        }

        [Test]
        public void Should_be_valid_when_length_is_equal_to_max_value()
        {
            var validator = new LengthValidator<Person>(2, 5);

            Assert.That(validator.Validate(null, "ABCDE"), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_length_is_lower_than_min_value()
        {
            var validator = new LengthValidator<Person>(2, 5);

            Assert.That(validator.Validate(null, "A"), Is.False);
        }

        [Test]
        public void Should_be_invalid_when_length_is_greater_than_min_value()
        {
            var validator = new LengthValidator<Person>(2, 5);

            Assert.That(validator.Validate(null, "ABCDEF"), Is.False);
        }

        [Test]
        public void Should_be_valid_when_length_is_between_values_using_instance_property()
        {
            var validator = new LengthValidator<Person>(p => p.Name.Length, p => p.Age);

            var person = new Person {Name = "Bob", Age = 8};
            Assert.That(validator.Validate(person, "ABCDE"), Is.True);
        }

        [Test]
        public void Should_be_valid_when_length_is_equal_to_min_value_using_instance_property()
        {
            var validator = new LengthValidator<Person>(p => p.Name.Length, p => p.Age);

            var person = new Person { Name = "Bob", Age = 8 };
            Assert.That(validator.Validate(person, "ABC"), Is.True);
        }

        [Test]
        public void Should_be_valid_when_length_is_equal_to_max_value_using_instance_property()
        {
            var validator = new LengthValidator<Person>(p => p.Name.Length, p => p.Age);

            var person = new Person { Name = "Bob", Age = 8 };
            Assert.That(validator.Validate(person, "ABCDEFGH"), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_length_is_lower_than_min_value_using_instance_property()
        {
            var validator = new LengthValidator<Person>(p => p.Name.Length, p => p.Age);

            var person = new Person { Name = "Bob", Age = 8 };
            Assert.That(validator.Validate(person, "AB"), Is.False);
        }

        [Test]
        public void Should_be_invalid_when_length_is_higher_than_max_value_using_instance_property()
        {
            var validator = new LengthValidator<Person>(p => p.Name.Length, p => p.Age);

            var person = new Person { Name = "Bob", Age = 8 };
            Assert.That(validator.Validate(person, "ABCDEFGHIJ"), Is.False);
        }

        [Test]
        public void Has_default_message()
        {
            var validator = new LengthValidator<Person>(5, 10);

            var message = validator.DefaultErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }

        [Test]
        public void Has_default_negated_message()
        {
            var validator = new LengthValidator<Person>(5, 10);

            var message = validator.DefaultNegatedErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }
    }
}
