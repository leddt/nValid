using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using nValid.Tests.TestObjects;
using nValid.Validators;

namespace nValid.Tests.Validators
{
    public class NegatedValidatorTests : TestBase
    {
        [Test]
        public void Should_be_valid_when_inner_validator_is_invalid()
        {
            var validator = CreateMock<IValidator<Person, string>>();
            var negated = new NegatedValidator<Person, string>(validator);
            var person = new Person();

            validator.Expect(v => v.Validate(person, "Test")).Return(false);

            Assert.That(negated.Validate(person, "Test"), Is.True);
        }

        [Test]
        public void Should_be_invalid_when_inner_validator_is_valid()
        {
            var validator = CreateMock<IValidator<Person, string>>();
            var negated = new NegatedValidator<Person, string>(validator);
            var person = new Person();

            validator.Expect(v => v.Validate(person, "Test")).Return(true);

            Assert.That(negated.Validate(person, "Test"), Is.False);
        }

        [Test]
        public void DefaultErrorMessage_should_return_inner_validator_DefaultNegatedErrorMessage()
        {
            var validator = CreateMock<IValidator<Person, string>>();
            var negated = new NegatedValidator<Person, string>(validator);

            validator.Stub(v => v.DefaultErrorMessage).Return("Message");
            validator.Stub(v => v.DefaultNegatedErrorMessage).Return("Negated Message");

            Assert.That(negated.DefaultErrorMessage, Is.EqualTo("Negated Message"));
        }

        [Test]
        public void DefaultNegatedErrorMessage_should_return_inner_validator_DefaultErrorMessage()
        {
            var validator = CreateMock<IValidator<Person, string>>();
            var negated = new NegatedValidator<Person, string>(validator);

            validator.Stub(v => v.DefaultErrorMessage).Return("Message");
            validator.Stub(v => v.DefaultNegatedErrorMessage).Return("Negated Message");

            Assert.That(negated.DefaultNegatedErrorMessage, Is.EqualTo("Message"));
        }
    }
}
