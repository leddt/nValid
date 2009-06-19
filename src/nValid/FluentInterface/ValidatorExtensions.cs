using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using nValid.Validators;

namespace nValid.FluentInterface
{
    public static class ValidatorExtensions
    {
        public static ValidatorNode<TInstance, TValue> Null<TInstance, TValue>(this IValidationNode<TInstance, TValue> node) where TValue : class
        {
            return node.AddValidator(new NullValidator<TInstance, TValue>());
        }

        public static ValidatorNode<TInstance, string> Empty<TInstance>(this IValidationNode<TInstance, string> node)
        {
            return node.AddValidator(new StringEmptyValidator<TInstance>());
        }

        public static ValidatorNode<TInstance, IList<TItem>> Empty<TInstance, TItem>(this IValidationNode<TInstance, IList<TItem>> node)
        {
            return node.AddValidator(new ListEmptyValidator<TInstance, TItem>());
        }

        public static ValidatorNode<TInstance, string> Length<TInstance>(this IValidationNode<TInstance, string> node, int minLength, int maxLength)
        {
            return node.AddValidator(new LengthValidator<TInstance>(minLength, maxLength));
        }
        public static ValidatorNode<TInstance, string> Length<TInstance>(this IValidationNode<TInstance, string> node, Expression<Func<TInstance, int>> minLength, Expression<Func<TInstance, int>> maxLength)
        {
            return node.AddValidator(new LengthValidator<TInstance>(minLength, maxLength));
        }

        public static ValidatorNode<TInstance, TValue> EqualTo<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, TValue valueToCompare) where TValue : IComparable<TValue>
        {
            return node.AddValidator(new EqualValidator<TInstance, TValue>(valueToCompare));
        }
        public static ValidatorNode<TInstance, TValue> EqualTo<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, Expression<Func<TInstance, TValue>> valueToCompare) where TValue : IComparable<TValue>
        {
            return node.AddValidator(new EqualValidator<TInstance, TValue>(valueToCompare));
        }

        public static ValidatorNode<TInstance, TValue> GreaterThan<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, TValue valueToCompare) where TValue : IComparable<TValue>
        {
            return node.AddValidator(new GreaterThanValidator<TInstance, TValue>(valueToCompare));
        }
        public static ValidatorNode<TInstance, TValue> GreaterThan<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, Expression<Func<TInstance, TValue>> valueToCompare) where TValue : IComparable<TValue>
        {
            return node.AddValidator(new GreaterThanValidator<TInstance, TValue>(valueToCompare));
        }

        public static ValidatorNode<TInstance, TValue> GreaterOrEqualTo<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, TValue valueToCompare) where TValue : IComparable<TValue>
        {
            return node.AddValidator(new GreaterOrEqualToValidator<TInstance, TValue>(valueToCompare));
        }
        public static ValidatorNode<TInstance, TValue> GreaterOrEqualTo<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, Expression<Func<TInstance, TValue>> valueToCompare) where TValue : IComparable<TValue>
        {
            return node.AddValidator(new GreaterOrEqualToValidator<TInstance, TValue>(valueToCompare));
        }

        public static ValidatorNode<TInstance, TValue> LowerThan<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, TValue valueToCompare) where TValue : IComparable<TValue>
        {
            return node.AddValidator(new LowerThanValidator<TInstance, TValue>(valueToCompare));
        }
        public static ValidatorNode<TInstance, TValue> LowerThan<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, Expression<Func<TInstance, TValue>> valueToCompare) where TValue : IComparable<TValue>
        {
            return node.AddValidator(new LowerThanValidator<TInstance, TValue>(valueToCompare));
        }

        public static ValidatorNode<TInstance, TValue> LowerOrEqualTo<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, TValue valueToCompare) where TValue : IComparable<TValue>
        {
            return node.AddValidator(new LowerOrEqualToValidator<TInstance, TValue>(valueToCompare));
        }
        public static ValidatorNode<TInstance, TValue> LowerOrEqualTo<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, Expression<Func<TInstance, TValue>> valueToCompare) where TValue : IComparable<TValue>
        {
            return node.AddValidator(new LowerOrEqualToValidator<TInstance, TValue>(valueToCompare));
        }

        public static ValidatorNode<TInstance, TValue> Custom<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, Func<TInstance, TValue, bool> validationFunction)
        {
            return node.AddValidator(new CustomValidator<TInstance, TValue>(validationFunction));
        }
        public static ValidatorNode<TInstance, TValue> Custom<TInstance, TValue>(this IValidationNode<TInstance, TValue> node, Predicate<TValue> validationFunction)
        {
            return node.AddValidator(new CustomValidator<TInstance, TValue>(validationFunction));
        }

        public static ValidatorNode<TInstance, string> Matches<TInstance>(this IValidationNode<TInstance, string> node, string pattern)
        {
            return node.AddValidator(new RegExValidator<TInstance>(pattern));
        }
    }
}
