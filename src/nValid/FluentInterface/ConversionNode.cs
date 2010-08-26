using System;
using nValid.Validators;

namespace nValid.FluentInterface
{
    public class ConversionNode<TInstance, TValue, TConvertedValue> : IValidationNode<TInstance, TConvertedValue>
    {
        private readonly RuleBuilder<TInstance> builder;
        private readonly Func<TInstance, TValue> valueFunction;
        private readonly string propertyKey;
        private readonly string propertyName;
        private readonly Func<TValue, TConvertedValue> converter;

        public ConversionNode(RuleBuilder<TInstance> builder, Func<TInstance, TValue> valueFunction, string propertyKey, string propertyName, Func<TValue, TConvertedValue> converter)
        {
            this.builder = builder;
            this.valueFunction = valueFunction;
            this.propertyKey = propertyKey;
            this.propertyName = propertyName;
            this.converter = converter;
        }

        public ValidatorNode<TInstance, TConvertedValue> AddValidator(IValidator<TInstance, TConvertedValue> validator)
        {
            return new ValidatorNode<TInstance, TConvertedValue>(
                builder,
                x => converter(valueFunction(x)),
                propertyKey,
                propertyName,
                validator
            );
        }

        public NegationNode<TInstance, TConvertedValue> Not
        {
            get { return new NegationNode<TInstance, TConvertedValue>(builder, x => converter(valueFunction(x)), propertyKey, propertyName); }
        }
    }
}