using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using nValid.Framework;
using nValid.Tests.TestObjects;
using nValid.Validators;

namespace nValid.Tests.Framework
{
    public class ValidatorRuleTests : TestBase
    {
        [Test]
        public void Should_validate_against_validator()
        {
            var validator = CreateMock<IValidator<Person, string>>();
            var person = new Person {Name = "Bob"};
            var rule = new ValidatorRule<Person, string>("", "", validator, p => p.Name);

            validator.Expect(v => v.Validate(person, "Bob")).Return(true);

            rule.Validate(person);

            VerifyAll();
        }

        [Test]
        public void Should_return_valid_result_if_validator_returned_valid()
        {
            var validator = CreateMock<IValidator<Person, string>>();
            var person = new Person { Name = "Bob" };
            var rule = new ValidatorRule<Person, string>("", "", validator, p => p.Name);

            validator.Stub(v => v.Validate(person, "Bob")).Return(true);

            var result = rule.Validate(person);

            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void Should_return_invalid_result_with_broken_rule_if_validator_returned_invalid()
        {
            var validator = CreateMock<IValidator<Person, string>>();
            var person = new Person { Name = "Bob" };
            var rule = new ValidatorRule<Person, string>("", "", validator, p => p.Name) { Message = "Invalid name" };

            validator.Stub(v => v.Validate(person, "Bob")).Return(false);

            var result = rule.Validate(person);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.BrokenRules[0].Message, Is.EqualTo("Invalid name"));
        }
    }
}
