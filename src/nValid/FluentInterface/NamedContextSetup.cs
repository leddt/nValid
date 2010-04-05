using System;
using nValid.Framework;

namespace nValid.FluentInterface
{
    public class NamedContextSetup
    {
        private readonly string contextName;

        public NamedContextSetup(string contextName)
        {
            this.contextName = contextName;
        }


        /// <summary>
        /// Setup the validation rules for type TInstance using the specified 
        /// configuration expression. The rules will be defined in the named
        /// context.
        /// </summary>
        /// <typeparam name="TInstance">
        /// The type for which to configure the validation rules.
        /// </typeparam>
        /// <param name="rules">
        /// A configuration expression describing the validation rules to apply 
        /// to TInstance.
        /// </param>
        public void For<TInstance>(Action<IRuleBuilder<TInstance>> rules)
        {
            using (new ValidationContextSwitch(contextName))
            {
                var builder = new RuleBuilder<TInstance>();
                rules(builder);

                ValidationContext.Current.AddRuleSet(builder.GetRules());
            }
        }
    }
}