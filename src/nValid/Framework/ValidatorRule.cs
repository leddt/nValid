using System;
using nValid.Validators;

namespace nValid.Framework
{
    public class ValidatorRule<TInstance, TValue> : ExpressionRuleBase<TInstance, TValue>
    {
        private readonly string propertyKey;
        private readonly string propertyName;
        private readonly IValidator<TInstance, TValue> validator;

        public override string Message
        {
            get
            {
                return !String.IsNullOrEmpty(base.Message) 
                    ? base.Message 
                    : validator.DefaultErrorMessage;
            }
            set { base.Message = value; }
        }

        public ValidatorRule(string propertyKey, string propertyName, IValidator<TInstance, TValue> validator, Func<TInstance, TValue> valueFunction) 
            : base(valueFunction)
        {
            this.propertyKey = propertyKey;
            this.propertyName = propertyName;
            this.validator = validator;
        }

        protected override RuleExecutionResult Validate(TInstance instance)
        {
            var value = GetValue(instance);
            var valid = validator.Validate(instance, GetValue(instance));
            
            var result = new RuleExecutionResult();
            if (!valid)
                result.BrokenRules.Add(new BrokenRule(this, instance, propertyKey, propertyName, value));

            return result;
        }
    }
}
