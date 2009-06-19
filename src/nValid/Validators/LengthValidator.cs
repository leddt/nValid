using System;
using System.Linq.Expressions;
using nValid.Framework;
using nValid.Utilities;

namespace nValid.Validators
{
    public class LengthValidator<TInstance> : IValidator<TInstance, string>
    {
        private readonly Func<TInstance, int> minLength;
        private readonly Func<TInstance, int> maxLength;
        private readonly string minString;
        private readonly string maxString;

        public string DefaultErrorMessage
        {
            get { return string.Format(ValidationContext.Current.GetResourceString("nValid_Length_DefaultMessage"), minString, maxString); }
        }

        public string DefaultNegatedErrorMessage
        {
            get { return string.Format(ValidationContext.Current.GetResourceString("nValid_Length_DefaultMessage_Negated"), minString, maxString); }
        }

        public LengthValidator(Expression<Func<TInstance, int>> minLength, Expression<Func<TInstance, int>> maxLength)
        {
            this.minLength = minLength.Compile();
            this.maxLength = maxLength.Compile();

            minString = minLength.GetMemberName();
            maxString = maxLength.GetMemberName();
        }

        public LengthValidator(int minLength, int maxLength)
        {
            this.minLength = i => minLength;
            this.maxLength = i => maxLength;

            minString = minLength.ToString();
            maxString = maxLength.ToString();
        }

        public bool Validate(TInstance instance, string value)
        {
            var len = (value ?? "").Length;

            return len >= minLength(instance) && len <= maxLength(instance);
        }
    }
}
