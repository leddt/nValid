using nValid.Framework;

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
            using (new ValidationContextSwitch(contextName))
            {
                return ValidationContext.Current.Validate(instance);
            }
        }
    }
}
