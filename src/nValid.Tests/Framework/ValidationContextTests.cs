using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using nValid.Framework;
using nValid.Tests.TestObjects;

namespace nValid.Tests.Framework
{
    public class ValidationContextTests : TestBase
    {
        private IRuleSet MockRuleSet<T>()
        {
            var set = CreateMock<IRuleSet>();
            set.Stub(r => r.ValidatedType).Return(typeof(T));

            return set;
        }

        protected override void FixtureSetUp()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }

        protected override void PerTestTearDown()
        {
            ValidationContext.Current = null;
        }

        [Test]
        public void Can_get_current_instance()
        {
            Assert.That(ValidationContext.Current, Is.Not.Null);
        }

        [Test]
        public void Current_instance_is_singleton()
        {
            var i1 = ValidationContext.Current;
            var i2 = ValidationContext.Current;

            Assert.That(i1, Is.SameAs(i2));
        }

        [Test]
        public void Can_replace_current_instance()
        {
            var context = new TestValidationContext();

            ValidationContext.Current = context;

            Assert.That(ValidationContext.Current, Is.SameAs(context));
        }

        [Test]
        public void Can_add_and_use_prebuilt_rule_set()
        {
            var context = new TestValidationContext();
            var set = MockRuleSet<Person>();
            context.AddRuleSet(set);

            var result = context.GetRuleSetsForType<Person>();

            Assert.That(result[0], Is.EqualTo(set));
        }

        [Test]
        public void Can_add_and_use_new_rule_set()
        {
            var context = new TestValidationContext();
            context.AddRuleSet<Person>(new List<IRule>());

            var result = context.GetRuleSetsForType<Person>();

            Assert.That(result[0], Is.Not.Null);
        }

        [Test]
        public void Should_use_all_matching_rule_sets()
        {
            var context = new TestValidationContext();
            context.AddRuleSet<Person>(new List<IRule>());
            context.AddRuleSet<Man>(new List<IRule>());
            context.AddRuleSet<IMale>(new List<IRule>());
            context.AddRuleSet<ISentient>(new List<IRule>());
            context.AddRuleSet<Item>(new List<IRule>()); // unrelated

            var results = context.GetRuleSetsForType<Man>();

            Assert.That(results.Count, Is.EqualTo(4));
        }

        [Test]
        public void Should_order_matching_rulesets_by_specificity()
        {
            var context = new TestValidationContext();
            context.AddRuleSet<Person>(new List<IRule>());
            context.AddRuleSet<Man>(new List<IRule>());
            context.AddRuleSet<IMale>(new List<IRule>());
            context.AddRuleSet<ISentient>(new List<IRule>());

            var results = context.GetRuleSetsForType<Man>();

            Assert.That(results[0].ValidatedType, Is.EqualTo(typeof(Man)));
            Assert.That(results[1].ValidatedType, Is.EqualTo(typeof(IMale)));
            Assert.That(results[2].ValidatedType, Is.EqualTo(typeof(Person)));
            Assert.That(results[3].ValidatedType, Is.EqualTo(typeof(ISentient)));
        }

        [Test]
        public void Should_cache_matching_rulesets()
        {
            var context = new TestValidationContext();

            var personRules = CreateMock<IRuleSet>();
            personRules.Expect(r => r.ValidatedType).Return(typeof (Person)).Repeat.Once();
            context.AddRuleSet(personRules);

            var r1 = context.GetRuleSetsForType<Person>();
            var r2 = context.GetRuleSetsForType<Person>();

            Assert.That(r1, Is.SameAs(r2));
            personRules.VerifyAllExpectations();
        }

        [Test]
        public void Should_validate_against_each_matching_rulesets()
        {
            var man = new Man();
            var ruleResult = new RuleExecutionResult();

            var context = new TestValidationContext();
            
            var personRules = MockRuleSet<Person>();
            personRules.Expect(r => r.Validate(man)).Return(ruleResult);
            context.AddRuleSet(personRules);

            var manRules = MockRuleSet<Man>();
            manRules.Expect(r => r.Validate(man)).Return(ruleResult);
            context.AddRuleSet(manRules);

            var maleRules = MockRuleSet<IMale>();
            maleRules.Expect(r => r.Validate(man)).Return(ruleResult);
            context.AddRuleSet(maleRules);

            var sentientRules = MockRuleSet<ISentient>();
            sentientRules.Expect(r => r.Validate(man)).Return(ruleResult);
            context.AddRuleSet(sentientRules);

            // unrelated
            var itemRules = MockRuleSet<Item>();
            itemRules.Expect(r => r.Validate(man)).Repeat.Never();
            context.AddRuleSet(itemRules);
            
            context.Validate(man);

            VerifyAll();
        }

        [Test]
        public void Can_stop_validation()
        {
            var man = new Man();
            var ruleResult = new RuleExecutionResult {StopEvaluation = true};

            var context = new TestValidationContext();

            var manRules = MockRuleSet<Man>();
            manRules.Expect(r => r.Validate(man)).Return(ruleResult);
            context.AddRuleSet(manRules);

            var personRules = MockRuleSet<Person>();
            personRules.Expect(r => r.Validate(man)).Repeat.Never();
            context.AddRuleSet(personRules);

            context.Validate(man);

            VerifyAll();
        }

        [Test]
        public void Validation_should_return_aggregated_broken_rules()
        {
            var man = new Man();
            var ruleResult1 = new RuleExecutionResult(new List<BrokenRule> { new BrokenRule(null, null, "", "", null) });
            var ruleResult2 = new RuleExecutionResult(new List<BrokenRule> { new BrokenRule(null, null, "", "", null), new BrokenRule(null, null, "", "", null) });

            var context = new TestValidationContext();

            var manRules = MockRuleSet<Man>();
            manRules.Expect(r => r.Validate(man)).Return(ruleResult1);
            context.AddRuleSet(manRules);

            var personRules = MockRuleSet<Person>();
            personRules.Expect(r => r.Validate(man)).Return(ruleResult2);
            context.AddRuleSet(personRules);

            var result = context.Validate(man);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.BrokenRules.Count, Is.EqualTo(3));
        }

        [Test]
        public void Can_get_resource_from_default_resource_manager()
        {
            var value = ValidationContext.Current.GetResourceString("nValid_Custom_DefaultMessage");

            Assert.That(value, Is.EqualTo("{Property} is invalid."));
        }

        [Test]
        public void Can_get_resource_from_custom_resource_manager()
        {
            ValidationContext.Current.ResourceManagers.Add(TestMessages.ResourceManager);

            var value = ValidationContext.Current.GetResourceString("nValid_Custom_DefaultMessage");

            Assert.That(value, Is.EqualTo("TEST CUSTOM DEFAULT"));
        }
    }
}
