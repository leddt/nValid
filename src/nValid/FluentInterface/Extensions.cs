using nValid.Framework;

namespace nValid.FluentInterface
{
    public static class Extensions
    {
        public static ValidationResult Validate<TInstance>(this TInstance instance)
        {
            return ValidationContext.Current.Validate(instance);
        }

        public static ValidationResult ValidateInContext<TInstance>(this TInstance instance, string contextName)
        {
            using (new ValidationContextSwitch(contextName))
            {
                return ValidationContext.Current.Validate(instance);
            }
        }
    }
}
