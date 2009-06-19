using System;
using System.Collections.Generic;
using System.Linq;

namespace nValid.Framework
{
    public class ForEachRule<TInstance, TItem> : ExpressionRuleBase<TInstance, IList<TItem>>
    {
        public IList<RuleBase<TItem>> Rules { get; private set; }

        public ForEachRule(Func<TInstance, IList<TItem>> valueFunction) 
            : base(valueFunction)
        {
            Rules = new List<RuleBase<TItem>>();
        }

        protected override RuleExecutionResult Validate(TInstance instance)
        {
            var ruleset = new RuleSet(typeof (TItem), Rules.Cast<IRule>());
            var items = GetValue(instance);
            var result = new RuleExecutionResult();

            foreach (var i in items)
            {
                var r = ruleset.Validate(i);
                
                foreach (var br in r.BrokenRules)
                    result.BrokenRules.Add(br);

                if (r.StopEvaluation)
                {
                    result.StopEvaluation = true;
                    break;
                }
            }

            return result;
        }
    }
}