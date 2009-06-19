using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using nValid.Framework;
using nValid.Validators;

namespace nValid.FluentInterface
{
    public class RuleBuilder<TInstance> : IRuleBuilder<TInstance>
    {
        private IList<IRule> Rules { get; set; }

        public ValidatorNode<TInstance, TInstance> AddValidator(IValidator<TInstance, TInstance> validator)
        {
            return new ValidatorNode<TInstance, TInstance>(this, i => i, typeof(TInstance).Name, typeof(TInstance).Name, validator);
        }

        public NegationNode<TInstance, TInstance> Not
        {
            get { return new NegationNode<TInstance, TInstance>(this, i => i, typeof(TInstance).Name, typeof(TInstance).Name); }
        }
        
        public RuleBuilder()
        {
            Rules = new List<IRule>();
        }

        public virtual void AddRule(RuleBase<TInstance> rule)
        {
            Rules.Add(rule);
        }

        public virtual IRuleSet GetRules()
        {
            return new RuleSet(typeof(TInstance), Rules);
        }

        public PropertyNode<TInstance, TValue> Property<TValue>(Expression<Func<TInstance, TValue>> propertyExpression)
        {
            return new PropertyNode<TInstance, TValue>(this, propertyExpression);
        }

        public IRuleBuilder<TItem> ForEach<TItem>(Func<TInstance, IList<TItem>> listExpression)
        {
            var rule = new ForEachRule<TInstance, TItem>(listExpression);
            AddRule(rule);

            return new ForEachRuleBuilder<TInstance, TItem>(rule);
        }
    }
}
