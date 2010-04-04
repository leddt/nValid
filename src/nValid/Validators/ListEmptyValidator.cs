using System.Collections.Generic;
using nValid.Framework;

namespace nValid.Validators
{
    public class ListEmptyValidator<TInstance, TItem> : IValidator<TInstance,IList<TItem>>
    {
        public string DefaultErrorMessage
        {
            get { return ValidationContext.GetResourceString("nValid_ListEmpty_DefaultMessage"); }
        }

        public string DefaultNegatedErrorMessage
        {
            get { return ValidationContext.GetResourceString("nValid_ListEmpty_DefaultMessage_Negated"); }
        }

        public bool Validate(TInstance instance, IList<TItem> value)
        {
            return value.Count == 0;
        }
    }
}