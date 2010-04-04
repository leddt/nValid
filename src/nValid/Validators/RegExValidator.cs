using System.Text.RegularExpressions;
using nValid.Framework;

namespace nValid.Validators
{
    public class RegExValidator<TInstance> : IValidator<TInstance, string>
    {
        private readonly Regex regex;

        public string DefaultErrorMessage
        {
            get { return ValidationContext.GetResourceString("nValid_RegEx_DefaultMessage"); }
        }

        public string DefaultNegatedErrorMessage
        {
            get { return ValidationContext.GetResourceString("nValid_RegEx_DefaultMessage_Negated"); }
        }

        public RegExValidator(string pattern)
        {
            regex = new Regex(pattern, RegexOptions.Compiled);
        }

        public bool Validate(TInstance instance, string value)
        {
            return regex.IsMatch(value);
        }
    }
}
