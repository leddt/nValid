using System;
using System.Collections.Generic;

namespace nValid.Framework
{
    public class RuleSet : IRuleSet
    {
        public Type ValidatedType { get; private set; }
        public IEnumerable<IRule> Rules { get; private set; }

        public RuleSet(Type validatedType, IEnumerable<IRule> rules)
        {
            ValidatedType = validatedType;
            Rules = rules;
        }

        public virtual RuleExecutionResult Validate(object instance)
        {
            var brokenRules = new List<BrokenRule>();
            var stop = false;

            foreach (var rule in Rules)
            {
                var r = rule.Validate(instance);
                if (!r.IsValid)
                    brokenRules.AddRange(r.BrokenRules);

                if (r.StopEvaluation)
                {
                    stop = true;
                    break;
                }
            }

            return new RuleExecutionResult(brokenRules)
                   {
                       StopEvaluation = stop
                   };
        }
    }
}
