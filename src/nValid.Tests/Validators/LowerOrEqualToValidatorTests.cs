using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Tests.TestObjects;
using nValid.Validators;

namespace nValid.Tests.Validators
{
    public class LowerOrEqualToValidatorTests : TestBase
    {
        [Test]
        public void Should_be_valid_when_value_is_equal()
        {
            var validator = new LowerOrEqualToValidator<Person, int>(18);

            Assert.That(validator.Validate(null, 18), Is.True);
        }

        [Test]
        public void Should_be_valid_when_value_is_lower()
        {
            var validator = new LowerOrEqualToValidator<Person, int>(18);

            Assert.That(validator.Validate(null, 16), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_value_is_greater()
        {
            var validator = new LowerOrEqualToValidator<Person, int>(18);

            Assert.That(validator.Validate(null, 21), Is.False);
        }

        [Test]
        public void Should_be_valid_when_value_is_equal_using_instance_property()
        {
            var validator = new LowerOrEqualToValidator<Person, int>(p => p.Age);

            var person = new Person { Age = 18 };
            Assert.That(validator.Validate(person, 18), Is.True);
        }

        [Test]
        public void Should_be_valid_when_value_is_lower_using_instance_property()
        {
            var validator = new LowerOrEqualToValidator<Person, int>(p => p.Age);

            var person = new Person { Age = 18 };
            Assert.That(validator.Validate(person, 16), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_value_is_greater_using_instance_property()
        {
            var validator = new LowerOrEqualToValidator<Person, int>(p => p.Age);
            
            var person = new Person {Age = 18};
            Assert.That(validator.Validate(person, 21), Is.False);
        }

        [Test]
        public void Has_default_message()
        {
            var validator = new LowerOrEqualToValidator<Person, int>(10);

            var message = validator.DefaultErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }

        [Test]
        public void Has_default_negated_message()
        {
            var validator = new LowerOrEqualToValidator<Person, int>(10);

            var message = validator.DefaultNegatedErrorMessage;
            Console.WriteLine(message);
            Assert.That(message, Is.Not.Null & Is.Not.Empty);
        }
    }
}
