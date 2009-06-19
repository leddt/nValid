using System.Collections.Generic;
using nValid.Framework;

namespace nValid.Tests.Framework
{
    public class TestConditionalRule<T> : ConditionalRuleBase<T>
    {
        private readonly bool valid;

        public TestConditionalRule(bool valid)
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