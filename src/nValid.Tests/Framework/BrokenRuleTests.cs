using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using nValid.Framework;
using nValid.Tests.TestObjects;

namespace nValid.Tests.Framework
{
    public class BrokenRuleTests : TestBase
    {
        [Test]
        public void Should_format_message()
        {
            var rule = CreateStub<IRule>();
            rule.Stub(r => r.Message).Return("{Instance.Name} has invalid {Property}: {Value}");

            var person = new Person {Name = "John Doe"};
            var brokenRule = new BrokenRule(rule, person, "Age", "Age", -123);

            var message = brokenRule.Message;

            Assert.That(message, Is.EqualTo("John Doe has invalid Age: -123"));
        }

        [Test]
        public void Can_use_resource_as_message()
        {
            ValidationContext.Current.ResourceManagers.Add(TestMessages.ResourceManager);

            var rule = CreateStub<IRule>();
            rule.Stub(r => r.Resource).Return("personInvalidName");

            var person = new Person();
            var brokenRule = new BrokenRule(rule, person, "Name", "Name", "John Doe");

            var message = brokenRule.Message;

            Assert.That(message, Is.EqualTo("John Doe is not a valid name."));
        }

        [Test]
        public void Can_access_broken_rule_details()
        {
            var rule = CreateStub<IRule>();
            var person = new Person();
            var brokenRule = new BrokenRule(rule, person, "FullName", "Full Name", -123);

            Assert.That(brokenRule.InvalidInstance, Is.EqualTo(person));
            Assert.That(brokenRule.InvalidValue, Is.EqualTo(-123));
            Assert.That(brokenRule.PropertyDisplayName, Is.EqualTo("Full Name"));
            Assert.That(brokenRule.PropertyKey, Is.EqualTo("FullName"));
        }
    }
}
