using System;

namespace nValid.Framework
{
    public abstract class RuleBase<TInstance> : IRule
    {
        public virtual string Message { get; set; }
        public virtual string Resource { get; set; }

        public virtual RuleExecutionResult Validate(object value)
        {
            if (value != null)
            {
                var valueType = value.GetType();
                var validatedType = typeof (TInstance);

                if (valueType != validatedType && !valueType.IsSubclassOf(validatedType))
                    throw new ArgumentException("Specified object cannot be validated by this rule.");
            }

            return DoValidation((TInstance)value);
        }

        protected virtual RuleExecutionResult DoValidation(TInstance value)
        {
            return Validate(value);
        }

        protected abstract RuleExecutionResult Validate(TInstance instance);
    }
}