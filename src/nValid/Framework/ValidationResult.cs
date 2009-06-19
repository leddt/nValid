using System.Collections.Generic;

namespace nValid.Framework
{
    public class ValidationResult
    {
        public IList<BrokenRule> BrokenRules { get; private set; }
        public bool IsValid { get; private set; }

        public ValidationResult()
        {
            BrokenRules = new List<BrokenRule>();
            IsValid = true;
        }

        public ValidationResult(IList<BrokenRule> brokenRules)
        {
            if (brokenRules != null)
            {
                BrokenRules = brokenRules;
                IsValid = brokenRules.Count == 0;
            }
            else
            {
                BrokenRules = new List<BrokenRule>();
                IsValid = true;
            }
        }
    }
}
