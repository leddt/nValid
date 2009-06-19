using System.Collections.Generic;
using nValid.Framework;

namespace nValid.Tests.Framework
{
    public class TestValidationContext : ValidationContext
    {
        public IList<IRuleSet> GetRuleSetsForType<T>()
        {
            return GetRulesetsFor(typeof (T));
        }
    }
}