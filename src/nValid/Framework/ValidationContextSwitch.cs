using System;

namespace nValid.Framework
{
    // Note: May not need this class after all. Since the fluent interface
    // exposes methods taking context name, actual users will not use this
    // class. The fluent interface could use GetNamedContext instead of
    // this to setup rules.
    // Removing this would also make ValidationContext simpler since there
    // would be no need for ThreadStatic or tracking the default context.
    // Must think further. TODO: Fix before release.
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
