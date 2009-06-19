using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using nValid.Framework;
using nValid.Tests.TestObjects;

namespace nValid.Tests.Framework
{
    public class ForEachRuleTests : TestBase
    {
        [Test]
        public void Should_validate_each_rule_against_each_object_in_list()
        {
            var person = new Person {Possessions = new List<Item>{new Item(), new Item(), new Item()}};

            var rule = new ForEachRule<Person, Item>(p => p.Possessions);
            rule.Rules.Add(CreateMock<RuleBase<Item>>());
            rule.Rules.Add(CreateMock<RuleBase<Item>>());

            rule.Rules[0].Expect(r => r.Validate(person.Possessions[0])).Return(new RuleExecutionResult());
            rule.Rules[1].Expect(r => r.Validate(person.Possessions[0])).Return(new RuleExecutionResult());

            rule.Rules[0].Expect(r => r.Validate(person.Possessions[1])).Return(new RuleExecutionResult());
            rule.Rules[1].Expect(r => r.Validate(person.Possessions[1])).Return(new RuleExecutionResult());

            rule.Rules[0].Expect(r => r.Validate(person.Possessions[2])).Return(new RuleExecutionResult());
            rule.Rules[1].Expect(r => r.Validate(person.Possessions[2])).Return(new RuleExecutionResult());

            rule.Validate(person);

            VerifyAll();
        }

        [Test]
        public void Can_stop()
        {
            var person = new Person { Possessions = new List<Item> { new Item(), new Item(), new Item() } };

            var rule = new ForEachRule<Person, Item>(p => p.Possessions);
            rule.Rules.Add(CreateMock<RuleBase<Item>>());
            rule.Rules.Add(CreateMock<RuleBase<Item>>());

            rule.Rules[0].Expect(r => r.Validate(person.Possessions[0])).Return(new RuleExecutionResult());
            rule.Rules[1].Expect(r => r.Validate(person.Possessions[0])).Return(new RuleExecutionResult());

            rule.Rules[0].Expect(r => r.Validate(person.Possessions[1])).Return(new RuleExecutionResult {StopEvaluation = true});
            rule.Rules[1].Expect(r => r.Validate(person.Possessions[1])).Repeat.Never();

            rule.Rules[0].Expect(r => r.Validate(person.Possessions[2])).Repeat.Never();
            rule.Rules[1].Expect(r => r.Validate(person.Possessions[2])).Repeat.Never();

            rule.Validate(person);

            VerifyAll();
        }

        [Test]
        public void Should_return_all_broken_rules()
        {
            var person = new Person { Possessions = new List<Item> { new Item(), new Item() } };

            var rule = new ForEachRule<Person, Item>(p => p.Possessions);
            rule.Rules.Add(CreateMock<RuleBase<Item>>());
            rule.Rules.Add(CreateMock<RuleBase<Item>>());

            rule.Rules[0].Expect(r => r.Validate(person.Possessions[0])).Return(new RuleExecutionResult(new List<BrokenRule> { new BrokenRule(null, null, "", "", null) }));
            rule.Rules[1].Expect(r => r.Validate(person.Possessions[0])).Return(new RuleExecutionResult(new List<BrokenRule> { new BrokenRule(null, null, "", "", null), new BrokenRule(null, null, "", "", null) }));

            rule.Rules[0].Expect(r => r.Validate(person.Possessions[1])).Return(new RuleExecutionResult(new List<BrokenRule> { new BrokenRule(null, null, "", "", null), new BrokenRule(null, null, "", "", null) }));
            rule.Rules[1].Expect(r => r.Validate(person.Possessions[1])).Return(new RuleExecutionResult(new List<BrokenRule> { new BrokenRule(null, null, "", "", null) }) { StopEvaluation = true });

            var result = rule.Validate(person);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.StopEvaluation, Is.True);
            Assert.That(result.BrokenRules.Count, Is.EqualTo(6));
        }
    }
}
