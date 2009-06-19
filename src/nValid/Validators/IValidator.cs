namespace nValid.Validators
{
    public interface IValidator<TInstance, TValue>
    {
        /// <summary>
        /// Validates the specified value of the specified instance.
        /// </summary>
        /// <param name="instance">The instance being validated.</param>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if the value is valid, otherwise <c>false</c>.</returns>
        bool Validate(TInstance instance, TValue value);

        string DefaultErrorMessage { get; }
        string DefaultNegatedErrorMessage { get; }
    }
}
