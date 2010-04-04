using nValid.Utilities.Formatting;

namespace nValid.Framework
{
    public class BrokenRule
    {
        private readonly IRule rule;
        private readonly object validatedInstance;
        private readonly string propertyName;
        private readonly object invalidValue;
        private readonly string propertyKey;

        public string Message
        {
            get 
            {
                string message = null;

                // Try and get message template from resource
                if (!string.IsNullOrEmpty(rule.Resource))
                    message = ValidationContext.GetResourceString(rule.Resource);

                // If no resource, get message template from rule
                if (string.IsNullOrEmpty(message))
                    message = rule.Message;

                // Format message template
                message = message.HaackFormat(new
                                              {
                                                  Instance = validatedInstance,
                                                  Value = invalidValue,
                                                  Property = propertyName
                                              });

                return message;
            }
        }

        public string PropertyKey
        {
            get { return propertyKey; }
        }

        public string PropertyDisplayName
        {
            get { return propertyName; }
        }

        public object InvalidInstance
        {
            get { return validatedInstance; }
        }

        public object InvalidValue
        {
            get { return invalidValue; }
        }

        public BrokenRule(IRule rule, object validatedInstance, string propertyKey, string propertyName, object invalidValue)
        {
            this.rule = rule;
            this.validatedInstance = validatedInstance;
            this.propertyKey = propertyKey;
            this.propertyName = propertyName;
            this.invalidValue = invalidValue;
        }
    }
}