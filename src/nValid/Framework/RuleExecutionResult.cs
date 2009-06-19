using System.Collections.Generic;

namespace nValid.Framework
{
    public class RuleExecutionResult
    {
        public IList<BrokenRule> BrokenRules { get; private set; }
        public bool StopEvaluation { get; set; }

        public bool IsValid
        {
            get
            {
                return BrokenRules == null || BrokenRules.Count == 0;
            }
        }

        public RuleExecutionResult()
        {
            BrokenRules = new List<BrokenRule>();
        }

        public RuleExecutionResult(IList<BrokenRule> brokenRules)
        {
            BrokenRules = brokenRules ?? new List<BrokenRule>();
        }
    }
}
