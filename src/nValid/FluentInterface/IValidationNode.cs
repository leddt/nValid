using nValid.Validators;

namespace nValid.FluentInterface
{
    public interface IValidationNode<TInstance, TValue>
    {
        /// <summary>
        /// Adds the specified <see cref="IValidator{TInstance,TValue}"/> to the current 
        /// <see cref="RuleBuilder{TInstance}"/>. Should not be used while configuring 
        /// rules. Intended for custom validator extensions.
        /// </summary>
        /// <param name="validator">
        /// The <see cref="IValidator{TInstance,TValue}"/> to add.
        /// </param>
        /// <returns>
        /// A <see cref="ValidatorNode{TInstance,TValue}"/> object, supporting the 
        /// fluent interface.
        /// </returns>
        ValidatorNode<TInstance, TValue> AddValidator(IValidator<TInstance, TValue> validator);

        /// <summary>
        /// Negates the next validator.
        /// </summary>
        NegationNode<TInstance, TValue> Not { get; }
    }
}
