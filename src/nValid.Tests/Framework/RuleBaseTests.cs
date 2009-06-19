using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Framework;
using nValid.Tests.TestObjects;

namespace nValid.Tests.Framework
{
    public class RuleBaseTests : TestBase
    {
        [Test]
        public void Can_validate_instance()
        {
            var person = new Person();
            
            var rule1 = new TestRule<Person>(true);
            var rule2 = new TestRule<Person>(false);

            var result1 = rule1.Validate(person);
            var result2 = rule2.Validate(person);

            Assert.That(result1.IsValid, Is.True);
            Assert.That(result2.IsValid, Is.False);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Validating_wrong_object_type_should_throw_exception()
        {
            var item = new Item();

            var rule = new TestRule<Person>(true);
            rule.Validate(item);
        }
    }

    internal class TestRule<T> : RuleBase<T>
    {
        private readonly bool valid;

        public TestRule(bool valid)
        {
            this.valid = valid;
        }

        protected override RuleExecutionResult Validate(T instance)
        {
            return valid
                       ? new RuleExecutionResult()
                       : new RuleExecutionResult(new List<BrokenRule> { new BrokenRule(this, instance, "", "", null) });
        }
    }
}
