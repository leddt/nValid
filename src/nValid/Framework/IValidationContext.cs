using System.Collections.Generic;
using System.Resources;

namespace nValid.Framework
{
    public interface IValidationContext
    {
        ValidationResult Validate(object instance);
        void AddRuleSet(IRuleSet set);
        void AddRuleSet<TInstance>(IList<IRule> rules);
        string GetResourceString(string key);
        IList<ResourceManager> ResourceManagers { get; }
    }
}