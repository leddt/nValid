using System;

namespace nValid.Framework
{
    public abstract class ConditionalRuleBase<TInstance> : RuleBase<TInstance>
    {
        public Predicate<TInstance> Condition { get; set; }
        
        protected override RuleExecutionResult DoValidation(TInstance value)
        {
            // If validation condition is not met, skip validation and return valid result.
            if (Condition != null && !Condition(value))
                return new RuleExecutionResult();

            return base.DoValidation(value);
        }
    }
}
