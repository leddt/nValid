using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace nValid.FluentInterface
{
    /// <summary>
    /// Provides a fluent interface for configuring validation rules for type TInstance.
    /// </summary>
    /// <typeparam name="TInstance">
    /// The type for which to configure the validation rules.
    /// </typeparam>
    public interface IRuleBuilder<TInstance> : IValidationNode<TInstance, TInstance>
    {
        /// <summary>
        /// Specifies that the following validators are for the specified 
        /// property of the validated instance.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the validated property.
        /// </typeparam>
        /// <param name="propertyExpression">
        /// An expression representing the property to validate.
        /// </param>
        /// <returns>
        /// A <see cref="PropertyNode{TInstance,TValue}"/> object, supporting the 
        /// fluent interface.
        /// </returns>
        PropertyNode<TInstance, TValue> Property<TValue>(Expression<Func<TInstance, TValue>> propertyExpression);

        /// <summary>
        /// Specifies that the following validators are to be applied to
        /// each element of the specified <see cref="IList{T}"/> of the
        /// validated instance.
        /// </summary>
        /// <typeparam name="TItem">
        /// The type of the validated items.
        /// </typeparam>
        /// <param name="listExpression">
        /// An expression representing the list of items to validate.
        /// </param>
        /// <returns>
        /// A <see cref="IRuleBuilder{TInstance}"/> object, allowing to specify
        /// the rules to apply to the list items.
        /// </returns>
        IRuleBuilder<TItem> ForEach<TItem>(Func<TInstance, IList<TItem>> listExpression);
    }
}