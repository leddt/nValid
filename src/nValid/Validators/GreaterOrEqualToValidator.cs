﻿using System;
using System.Linq.Expressions;
using nValid.Framework;
using nValid.Utilities;

namespace nValid.Validators
{
    public class GreaterOrEqualToValidator<TInstance, TValue> : IValidator<TInstance, TValue> where TValue : IComparable<TValue>
    {
        private readonly Func<TInstance, TValue> valueToCompare;
        private readonly string valueString;

        public string DefaultErrorMessage
        {
            get { return string.Format(ValidationContext.Current.GetResourceString("nValid_GreaterOrEqualTo_DefaultMessage"), valueString); }
        }

        public string DefaultNegatedErrorMessage
        {
            get { return string.Format(ValidationContext.Current.GetResourceString("nValid_GreaterOrEqualTo_DefaultMessage_Negated"), valueString); }
        }

        public GreaterOrEqualToValidator(Expression<Func<TInstance, TValue>> valueToCompare)
        {
            this.valueToCompare = valueToCompare.Compile();
            valueString = valueToCompare.GetMemberName();
        }

        public GreaterOrEqualToValidator(TValue valueToCompare)
        {
            this.valueToCompare = i => valueToCompare;
            valueString = valueToCompare.ToString();
        }

        public bool Validate(TInstance instance, TValue value)
        {
            return value.CompareTo(valueToCompare(instance)) >= 0;
        }
    }
}
