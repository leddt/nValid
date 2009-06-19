using System;
using nValid.Framework;

namespace nValid.Validators
{
    public class CustomValidator<TInstance, TValue> : IValidator<TInstance, TValue>
    {
        private readonly Func<TInstance, TValue, bool> validationFunction;

        public string DefaultErrorMessage
        {
            get { return ValidationContext.Current.GetResourceString("nValid_Custom_DefaultMessage"); }
        }

        public string DefaultNegatedErrorMessage
        {
            get { return ValidationContext.Current.GetResourceString("nValid_Custom_DefaultMessage_Negated"); }
        }

        public CustomValidator(Func<TInstance, TValue, bool> validationFunction)
        {
            this.validationFunction = validationFunction;
        }

        public CustomValidator(Predicate<TValue> validationFunction)
        {
            this.validationFunction = (i, v) => validationFunction(v);
        }

        public bool Validate(TInstance instance, TValue value)
        {
            return validationFunction(instance, value);
        }
    }
}
