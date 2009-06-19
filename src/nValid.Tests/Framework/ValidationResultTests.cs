using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using nValid.Framework;

namespace nValid.Tests.Framework
{
    public class ValidationResultTests : TestBase
    {
        [Test]
        public void Default_constructor_should_create_valid_instance()
        {
            var result = new ValidationResult();

            Assert.That(result.IsValid, Is.True);
            Assert.That(result.BrokenRules, Is.Empty);
        }

        [Test]
        public void Passing_null_broken_rules_should_create_valid_instance()
        {
            var result = new ValidationResult(null);

            Assert.That(result.IsValid, Is.True);
            Assert.That(result.BrokenRules, Is.Empty);
        }

        [Test]
        public void Passing_empty_broken_rules_should_create_valid_instance()
        {
            var result = new ValidationResult(new List<BrokenRule>());

            Assert.That(result.IsValid, Is.True);
            Assert.That(result.BrokenRules, Is.Empty);
        }

        [Test]
        public void Passing_filled_broken_rules_should_create_invalid_instance()
        {
            var result = new ValidationResult(new List<BrokenRule> { new BrokenRule(null, null, "", "", null) });

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.BrokenRules.Count, Is.EqualTo(1));
        }
    }
}
