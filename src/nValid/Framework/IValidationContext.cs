using System.Collections.Generic;

namespace nValid.Framework
{
    public interface IValidationContext
    {
        ValidationResult Validate(object instance);
        void AddRuleSet(IRuleSet set);
        void AddRuleSet<TInstance>(IList<IRule> rules);
    }
}