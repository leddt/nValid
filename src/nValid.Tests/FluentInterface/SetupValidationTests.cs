using System;
using System.Linq;
using System.Resources;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.FluentInterface;
using nValid.Framework;
using nValid.Tests.Framework;
using nValid.Tests.TestObjects;

namespace nValid.Tests.FluentInterface
{
    public class SetupValidationTests : TestBase
    {
        private TestValidationContext context;
        
        protected override void PerTestSetUp()
        {
            context = new TestValidationContext();
            ValidationContext.Current = context;
        }

        protected override void PerTestTearDown()
        {
            ValidationContext.Reset();
        }

        [Test]
        public void Can_setup_rules_for_type()
        {
            SetupValidation.For<Person>(rules => {});

            Assert.That(context.GetRuleSetsForType<Person>().Count, Is.EqualTo(1));
        }

        [Test]
        public void Can_add_self_referencing_validator()
        {
            SetupValidation.For<Person>(rules => rules.Null());

            var rule = context.GetRuleSetsForType<Person>()[0].Rules.First() as ValidatorRule<Person, Person>;
            Assert.That(rule, Is.Not.Null);
            Assert.That(rule.Validate(null).IsValid, Is.True);
        }

        [Test]
        public void Can_add_self_referencing_negated_validator()
        {
            SetupValidation.For<Person>(rules => rules.Not.Null());

            var rule = context.GetRuleSetsForType<Person>()[0].Rules.First() as ValidatorRule<Person, Person>;
            Assert.That(rule.Validate(null).IsValid, Is.False);
        }

        [Test]
        public void Can_add_validator_rule_for_property()
        {
            SetupValidation.For<Person>(rules => rules.Property(p => p.Name).Empty());
            var person = new Person {Name = ""};

            var rule = context.GetRuleSetsForType<Person>()[0].Rules.First() as ValidatorRule<Person, string>;
            Assert.That(rule.Validate(person).IsValid, Is.True);
        }

        [Test]
        public void Can_add_negated_validator_rule_for_property()
        {
            SetupValidation.For<Person>(rules => rules.Property(p => p.Name).Not.Empty());
            var person = new Person { Name = "" };

            var rule = context.GetRuleSetsForType<Person>()[0].Rules.First() as ValidatorRule<Person, string>;
            Assert.That(rule.Validate(person).IsValid, Is.False);
        }

        [Test]
        public void Can_chain_validators()
        {
            SetupValidation.For<Person>(rules => 
                rules.Property(p => p.Name)
                    .Length(1, 10)
                    .Matches("^[A-Z][a-z]+$")
            );

            var person = new Person {Name = "JOhn"};

            var r1 = context.GetRuleSetsForType<Person>()[0].Rules.ElementAt(0) as ValidatorRule<Person, string>;
            var r2 = context.GetRuleSetsForType<Person>()[0].Rules.ElementAt(1) as ValidatorRule<Person, string>;

            Assert.That(r1.Validate(person).IsValid, Is.True);
            Assert.That(r2.Validate(person).IsValid, Is.False);
        }

        [Test]
        public void Can_chain_negated_validators()
        {
            SetupValidation.For<Person>(rules =>
                rules.Property(p => p.Name)
                    .Not.Length(4, 6)
                    .Not.Matches("[A-Z][a-z]+")
            );

            var person = new Person { Name = "Johnnyboy" };

            var r1 = context.GetRuleSetsForType<Person>()[0].Rules.ElementAt(0) as ValidatorRule<Person, string>;
            var r2 = context.GetRuleSetsForType<Person>()[0].Rules.ElementAt(1) as ValidatorRule<Person, string>;

            Assert.That(r1.Validate(person).IsValid, Is.True);
            Assert.That(r2.Validate(person).IsValid, Is.False);
        }

        [Test]
        public void Can_add_validator_rule_for_each_item_of_collection()
        {
            SetupValidation.For<Person>(rules => rules.ForEach(p => p.Possessions).Property(i => i.Weight).GreaterThan(0));

            var rule = context.GetRuleSetsForType<Person>()[0].Rules.First() as ForEachRule<Person, Item>;
            var valRule = rule.Rules[0] as ValidatorRule<Item, float>;
            Assert.That(valRule, Is.Not.Null);
        }

        [Test]
        public void Can_set_message_on_validator()
        {
            SetupValidation.For<Person>(rules => rules.Property(p => p.Name).Empty().WithMessage("Test"));

            var rule = context.GetRuleSetsForType<Person>().First().Rules.First() as ValidatorRule<Person, string>;
            Assert.That(rule.Message, Is.EqualTo("Test"));
        }

        [Test]
        public void Can_set_resource_on_validator()
        {
            SetupValidation.For<Person>(rules => rules.Property(p => p.Name).Empty().WithResource("Test"));

            var rule = context.GetRuleSetsForType<Person>().First().Rules.First() as ValidatorRule<Person, string>;
            Assert.That(rule.Resource, Is.EqualTo("Test"));
        }

        [Test]
        public void Can_set_condition_on_validator()
        {
            var condition = new Predicate<Person>(p => true);
            SetupValidation.For<Person>(rules => rules.Property(p => p.Name).Empty().When(condition));

            var rule = context.GetRuleSetsForType<Person>()[0].Rules.First() as ValidatorRule<Person, string>;
            Assert.That(rule.Condition, Is.SameAs(condition));
        }

        [Test]
        public void Can_add_resource_manager()
        {
            var rm = new ResourceManager(GetType());
            SetupValidation.AddResourceManager(rm);

            Assert.That(ValidationContext.ResourceManagers.Count, Is.EqualTo(1));
            Assert.That(ValidationContext.ResourceManagers[0], Is.SameAs(rm));
        }

        [Test]
        public void Can_add_new_resource_manager()
        {
            SetupValidation.AddResourceManager("resources", GetType().Assembly);

            Assert.That(ValidationContext.ResourceManagers.Count, Is.EqualTo(1));
            Assert.That(ValidationContext.ResourceManagers[0].BaseName, Is.EqualTo("resources"));
        }

        [Test]
        public void Can_reset_validation_context()
        {
            SetupValidation.AddResourceManager(new ResourceManager(GetType()));
            SetupValidation.For<Person>(rules => rules.Custom(p => false));
            SetupValidation.Reset();

            Assert.That(ValidationContext.ResourceManagers.Count, Is.EqualTo(0));
            Assert.That(ValidationContext.Current.Validate(new Person()).IsValid, Is.True);
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void Cannot_negate_a_negation()
        {
            SetupValidation.For<Person>(rules => rules.Not.Not.Null());
        }

        [Test]
        public void Can_set_property_name()
        {
            SetupValidation.For<Person>(rules => rules.Property(p => p.Name).WithName("Full name").Empty());

            var rule = (ValidatorRule<Person, string>)context.GetRuleSetsForType<Person>().First().Rules.First();
            Assert.That(rule.Validate(new Person {Name = "invalid"}).BrokenRules.First().PropertyDisplayName, Is.EqualTo("Full name"));
        }

        [Test]
        public void Can_set_property_key()
        {
            SetupValidation.For<Person>(rules => rules.Property(p => p.Name).WithKey("_name").Empty());

            var rule = (ValidatorRule<Person, string>)context.GetRuleSetsForType<Person>().First().Rules.First();
            Assert.That(rule.Validate(new Person { Name = "invalid" }).BrokenRules.First().PropertyKey, Is.EqualTo("_name"));
        }
    }
}
