using System;
using System.Linq.Expressions;
using nValid.Utilities;
using nValid.Validators;

namespace nValid.FluentInterface
{
    public class PropertyNode<TInstance, TValue> : IValidationNode<TInstance, TValue>
    {
        private readonly Func<TInstance, TValue> valueFunction;
        private string propertyKey;
        private string propertyName;
        private readonly RuleBuilder<TInstance> builder;

        public PropertyNode(RuleBuilder<TInstance> builder, Expression<Func<TInstance, TValue>> valueExpression)
        {
            this.builder = builder;
            valueFunction = valueExpression.Compile();
            propertyKey = valueExpression.GetMemberKey();
            propertyName = valueExpression.GetMemberName();
        }

        public PropertyNode<TInstance, TValue> WithKey(string newKey)
        {
            propertyKey = newKey;
            return this;
        }

        public PropertyNode<TInstance, TValue> WithName(string newName)
        {
            propertyName = newName;
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
