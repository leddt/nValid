using nValid.Framework;

namespace nValid.Validators
{
    public class NullValidator<TInstance, TValue> : IValidator<TInstance, TValue> where TValue : class
    {
        public string DefaultErrorMessage
        {
            get { return ValidationContext.Current.GetResourceString("nValid_Null_DefaultMessage"); }
        }

        public string DefaultNegatedErrorMessage
        {
            get { return ValidationContext.Current.GetResourceString("nValid_Null_DefaultMessage_Negated"); }
        }

        public bool Validate(TInstance instance, TValue value)
        {
            return value == null;
        }
    }
}