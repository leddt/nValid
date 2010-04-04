using System;
using System.Reflection;
using System.Resources;
using nValid.Framework;

namespace nValid.FluentInterface
{
    /// <summary>
    /// Entry point for the validation configuration fluent interface.
    /// </summary>
    public static class SetupValidation
    {
        /// <summary>
        /// Adds a <see cref="System.Resources.ResourceManager"/> to the list of 
        /// resource managers used to get validation messages.
        /// </summary>
        /// <remarks>
        /// The resource managers are accessed in the order they were added. 
        /// As soon as matching resource is found, it is used and any remaining 
        /// resource manager is ignored.
        /// </remarks>
        /// <param name="resourceManager">
        /// The <see cref="System.Resources.ResourceManager"/> instance to add.
        /// </param>
        public static void AddResourceManager(ResourceManager resourceManager)
        {
            ValidationContext.ResourceManagers.Add(resourceManager);
        }

        /// <summary>
        /// Adds a <see cref="System.Resources.ResourceManager"/> to the list of 
        /// resource managers used to get validation messages.
        /// </summary>
        /// <remarks>
        /// The resource managers are accessed in the order they were added. 
        /// As soon as matching resource is found, it is used and any remaining 
        /// resource manager is ignored.
        /// </remarks>
        /// <param name="baseName">
        /// The root name of the resources. For example, the base name for the
        /// resource file named "MyResource.en-US.resources" is "MyResource"
        /// </param>
        /// <param name="assembly">
        /// The main assembly for the resources.
        /// </param>
        public static void AddResourceManager(string baseName, Assembly assembly)
        {
            ValidationContext.ResourceManagers.Add(new ResourceManager(baseName, assembly));
        }

        /// <summary>
        /// Setup the validation rules for type TInstance using the specified 
        /// configuration expression.
        /// </summary>
        /// <typeparam name="TInstance">
        /// The type for which to configure the validation rules.
        /// </typeparam>
        /// <param name="rules">
        /// A configuration expression describing the validation rules to apply 
        /// to TInstance.
        /// </param>
        public static void For<TInstance>(Action<IRuleBuilder<TInstance>> rules)
        {
            var builder = new RuleBuilder<TInstance>();
            rules(builder);

            ValidationContext.Current.AddRuleSet(builder.GetRules());
        }

        /// <summary>
        /// Clears any and all configuration data and rule sets for the validation system.
        /// </summary>
        public static void Reset()
        {
            ValidationContext.Reset();
        }
    }
}