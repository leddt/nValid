using nValid.Framework;

namespace nValid.FluentInterface
{
    public class ForEachRuleBuilder<TInstance, TItem> : RuleBuilder<TItem>
    {
        private readonly ForEachRule<TInstance, TItem> parentRule;

        public ForEachRuleBuilder(ForEachRule<TInstance, TItem> parentRule)
        {
            this.parentRule = parentRule;
        }

        public override void AddRule(RuleBase<TItem> rule)
        {
            parentRule.Rules.Add(rule);
        }
    }
}