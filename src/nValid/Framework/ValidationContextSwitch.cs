using System;

namespace nValid.Framework
{
    public class ValidationContextSwitch : IDisposable
    {
        private readonly IValidationContext originalContext;

        public ValidationContextSwitch(string contextName)
        {
            originalContext = ValidationContext.Current;

            ValidationContext.Current = ValidationContext.GetNamedContext(contextName);
        }

        public void Dispose()
        {
            ValidationContext.Current = originalContext;
        }
    }
}
