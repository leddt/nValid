using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using nValid.FluentInterface;
using nValid.Framework;
using nValid.Tests.TestObjects;

namespace nValid.Tests.FluentInterface
{
    public class ValidationTests : TestBase
    {
        protected override void PerTestTearDown()
        {
            ValidationContext.Current = null;
        }

        [Test]
        public void Should_validate_against_current_validation_context()
        {
            var person = new Person();
            var expectedResult = new ValidationResult();

            var context = CreateMock<IValidationContext>();
            ValidationContext.Current = context;

            context.Expect(c => c.Validate(person)).Return(expectedResult);

            var result = Validate.Instance(person);

            Assert.That(result, Is.SameAs(expectedResult));

            VerifyAll();
        }

        [Test]
        public void Should_validate_against_current_validation_context_using_alternate_syntax()
        {
            var person = new Person();
            var expectedResult = new ValidationResult();

            var context = CreateMock<IValidationContext>();
            ValidationContext.Current = context;

            context.Expect(c => c.Validate(person)).Return(expectedResult);

            var result = person.Validate();

            Assert.That(result, Is.SameAs(expectedResult));

            VerifyAll();
        }
    }
}
