using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Core.Validation;

/// <summary>
/// Defines a builder for creating validation rules for a specific property of a type.
/// </summary>
/// <typeparam name="TRequest">The type of the object being validated.</typeparam>
/// <typeparam name="TProperty">The type of the property being validated.</typeparam>
internal interface IRuleBuilder<TRequest, TProperty>
{
    /// <summary>Adds a validation condition. Fails when <paramref name="predicate"/> returns false.</summary>
    IRuleBuilder<TRequest, TProperty> Must(Func<TProperty, bool> predicate, string defaultMessage);

    /// <summary>Overrides the error message of the most recently added condition.</summary>
    IRuleBuilder<TRequest, TProperty> WithMessage(string message);

    /// <summary>Only runs this rule's conditions when <paramref name="condition"/> returns true.</summary>
    IRuleBuilder<TRequest, TProperty> When(Func<TRequest, bool> condition);

    /// <summary>Sets a custom validator for the property.</summary>
    IRuleBuilder<TRequest, TProperty> SetValidator(AbstractValidator<TProperty> validator);
}

