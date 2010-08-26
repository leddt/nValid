using System;

namespace nValid.Framework
{
    public abstract class ExpressionRuleBase<TInstance, TValue> : ConditionalRuleBase<TInstance>
    {
        private readonly Func<TInstance, TValue> valueFunction;

        protected ExpressionRuleBase(Func<TInstance, TValue> valueFunction)
        {
            this.valueFunction = valueFunction;
        }

        protected TValue GetValue(TInstance instance)
        {
            return valueFunction(instance);
        }

        protected bool TryGetValue(TInstance instance, out TValue value)
        {
            try
            {
                value = valueFunction(instance);
                return true;
            }
            catch
            {
                value = default(TValue);
                return false;
            }
        }
    }
}
