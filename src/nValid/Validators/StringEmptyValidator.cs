using System;
using nValid.Framework;

namespace nValid.Validators
{
    public class StringEmptyValidator<TInstance> : IValidator<TInstance, string>
    {
        public string DefaultErrorMessage
        {
            get { return ValidationContext.Current.GetResourceString("nValid_StringEmpty_DefaultMessage"); }
        }

        public string DefaultNegatedErrorMessage
        {
            get { return ValidationContext.Current.GetResourceString("nValid_StringEmpty_DefaultMessage_Negated"); }
        }

        public bool Validate(TInstance instance, string value)
        {
            return String.IsNullOrEmpty(value);
        }
    }
}