namespace nValid.Validators
{
    public class NegatedValidator<TInstance, TValue> : IValidator<TInstance, TValue>
    {
        private readonly IValidator<TInstance, TValue> inner;

        public NegatedValidator(IValidator<TInstance, TValue> inner)
        {
            this.inner = inner;
        }

        public bool Validate(TInstance instance, TValue value)
        {
            return !inner.Validate(instance, value);
        }

        public string DefaultErrorMessage
        {
            get { return inner.DefaultNegatedErrorMessage; }
        }

        public string DefaultNegatedErrorMessage
        {
            get { return inner.DefaultErrorMessage; }
        }
    }
}