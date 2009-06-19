using System;
using nValid.Validators;

namespace nValid.FluentInterface
{
    public class NegationNode<TInstance, TValue> : IValidationNode<TInstance, TValue>
    {
        private readonly RuleBuilder<TInstance> builder;
        private readonly Func<TInstance, TValue> valueFunction;
        private readonly string propertyKey;
        private readonly string propertyName;

        public NegationNode(RuleBuilder<TInstance> builder, Func<TInstance, TValue> valueFunction, string propertyKey, string propertyName)
        {
            this.builder = builder;
            this.valueFunction = valueFunction;
            this.propertyKey = propertyKey;
            this.propertyName = propertyName;
        }

        public ValidatorNode<TInstance, TValue> AddValidator(IValidator<TInstance, TValue> validator)
        {
            return new ValidatorNode<TInstance, TValue>(
                builder, 
                valueFunction, 
                propertyKey,
                propertyName, 
                new NegatedValidator<TInstance, TValue>(validator)
            );
        }

        public NegationNode<TInstance, TValue> Not
        {
            get { throw new InvalidOperationException("Don't use 'Not' twice in a row."); }
        }
    }
}