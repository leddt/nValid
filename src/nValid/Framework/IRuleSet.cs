using System;
using System.Collections.Generic;

namespace nValid.Framework
{
    public interface IRuleSet
    {
        Type ValidatedType { get; }
        IEnumerable<IRule> Rules { get; }
        RuleExecutionResult Validate(object instance);
    }
}