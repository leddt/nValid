using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Tests.TestObjects;

namespace nValid.Tests.Framework
{
    public class ConditionalRuleBaseTests : TestBase
    {
        [Test]
        public void Should_always_be_valid_if_condition_is_not_met()
        {
            var person = new Person();

            var rule1 = new TestConditionalRule<Person>(true) { Condition = (p => false) };
            var rule2 = new TestConditionalRule<Person>(false) { Condition = (p => false) };

            var result1 = rule1.Validate(person);
            var result2 = rule2.Validate(person);

            Assert.That(result1.IsValid, Is.True);
            Assert.That(result2.IsValid, Is.True);
        }

        [Test]
        public void Should_run_validation_when_condition_is_met()
        {
            var person = new Person();

            var rule1 = new TestConditionalRule<Person>(true) { Condition = (p => true) };
            var rule2 = new TestConditionalRule<Person>(false) { Condition = (p => true) };

            var result1 = rule1.Validate(person);
            var result2 = rule2.Validate(person);

            Assert.That(result1.IsValid, Is.True);
            Assert.That(result2.IsValid, Is.False);
        }

        [Test]
        public void Should_run_validation_when_condition_is_not_set()
        {
            var person = new Person();

            var rule1 = new TestConditionalRule<Person>(true);
            var rule2 = new TestConditionalRule<Person>(false);

            var result1 = rule1.Validate(person);
            var result2 = rule2.Validate(person);

            Assert.That(result1.IsValid, Is.True);
            Assert.That(result2.IsValid, Is.False);
        }
    }
}
