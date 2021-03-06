﻿using nValid.Framework;

namespace nValid.FluentInterface
{
    public static class Validate
    {
        public static ValidationResult Instance<TInstance>(TInstance instance)
        {
            return ValidationContext.Current.Validate(instance);
        }

        public static ValidationResult InstanceInContext<TInstance>(TInstance instance, string contextName)
        {
            return ValidationContext.GetNamedContext(contextName).Validate(instance);
        }
    }
}
