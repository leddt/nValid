using System;
using nValid.Framework;
using nValid.Validators;

namespace nValid.FluentInterface
{
    public class ValidatorNode<TInstance, TValue> : IValidationNode<TInstance, TValue>
    {
        public IValidator<TInstance, TValue> Validator { get; private set; }

        private readonly RuleBuilder<TInstance> builder;
        private readonly Func<TInstance, TValue> valueFunction;
        private readonly string propertyKey;
        private readonly string propertyName;
        private ValidatorRule<TInstance, TValue> Rule { get; set; }

        public ValidatorNode(RuleBuilder<TInstance> builder, Func<TInstance, TValue> valueFunction, string propertyKey, string propertyName, IValidator<TInstance, TValue> validator)
        {
            this.builder = builder;
            this.valueFunction = valueFunction;
            this.propertyKey = propertyKey;
            this.propertyName = propertyName;

            Validator = validator;
            Rule = new ValidatorRule<TInstance, TValue>(propertyKey, propertyName, validator, valueFunction);

            builder.AddRule(Rule);
        }

        public ValidatorNode<TInstance, TValue> WithMessage(string message)
        {
            Rule.Message = message;
            Rule.Resource = null;
            
            return this;
        }

        public ValidatorNode<TInstance, TValue> WithResource(string key)
        {
            Rule.Message = null;
            Rule.Resource = key;

            return this;
        }

        public ValidatorNode<TInstance, TValue> When(Predicate<TInstance> condition)
        {
            Rule.Condition = condition;

            return this;
        }

        public ValidatorNode<TInstance, TValue> AddValidator(IValidator<TInstance, TValue> validator)
        {
            return new ValidatorNode<TInstance, TValue>(builder, valueFunction, propertyKey, propertyName, validator);
        }

        public NegationNode<TInstance, TValue> Not
        {
            get { return new NegationNode<TInstance, TValue>(builder, valueFunction, propertyKey, propertyName); }
        }
    }
}
