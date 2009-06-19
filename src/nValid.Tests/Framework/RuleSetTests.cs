using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using nValid.Framework;
using nValid.Tests.TestObjects;

namespace nValid.Tests.Framework
{
    public class RuleSetTests : TestBase
    {
        [Test]
        public void Should_validate_against_each_rule_if_not_stopped()
        {
            var person = new Person();

            var rules = new List<IRule>
                        {
                            CreateMock<IRule>(),
                            CreateMock<IRule>(),
                            CreateMock<IRule>()
                        };

            rules[0].Expect(r => r.Validate(person)).Return(new RuleExecutionResult());
            rules[1].Expect(r => r.Validate(person)).Return(new RuleExecutionResult());
            rules[2].Expect(r => r.Validate(person)).Return(new RuleExecutionResult());

            var ruleSet = new RuleSet(typeof (Person), rules);

            ruleSet.Validate(person);

            VerifyAll();
        }

        [Test]
        public void Can_stop()
        {
            var person = new Person();

            var rules = new List<IRule>
                        {
                            CreateMock<IRule>(),
                            CreateMock<IRule>(),
                            CreateMock<IRule>()
                        };

            rules[0].Stub(r => r.Validate(person)).Return(new RuleExecutionResult());
            rules[1].Stub(r => r.Validate(person)).Return(new RuleExecutionResult {StopEvaluation = true});
            rules[2].Expect(r => r.Validate(person)).Repeat.Never();

            var ruleSet = new RuleSet(typeof(Person), rules);

            ruleSet.Validate(person);

            VerifyAll();
        }

        [Test]
        public void Should_aggregate_results()
        {
            var person = new Person();

            var rules = new List<IRule>
                        {
                            CreateMock<IRule>(),
                            CreateMock<IRule>()
                        };

            rules[0].Stub(r => r.Validate(person)).Return(new RuleExecutionResult(new List<BrokenRule> { new BrokenRule(null, null, "", "", null), new BrokenRule(null, null, "", "", null) }));
            rules[1].Stub(r => r.Validate(person)).Return(new RuleExecutionResult(new List<BrokenRule> { new BrokenRule(null, null, "", "", null) }) { StopEvaluation = true });

            var ruleSet = new RuleSet(typeof(Person), rules);

            var result = ruleSet.Validate(person);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.BrokenRules.Count, Is.EqualTo(3));
        }
    }
}
