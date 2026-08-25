using CourierHub.Core.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace CourierHub.Core.Validation;

/// <summary>
/// Represents a validation rule for a specific property of a request object.
/// </summary>
/// <typeparam name="TRequest">The type of the request object.</typeparam>
/// <typeparam name="TProperty">The type of the property being validated.</typeparam>
internal sealed class PropertyRule<TRequest, TProperty> : PropertyRuleBase<TRequest>, IRuleBuilder<TRequest, TProperty>
{
    /// <summary>
    /// The name of the property being validated.
    /// </summary>
    private readonly string _propertyName;

    /// <summary>
    /// A function to get the value of the property from the request object.
    /// </summary>
    private readonly Func<TRequest, TProperty> _getter;

    /// <summary>
    /// A list of validation conditions, each consisting of a predicate and an associated error message.
    /// </summary>
    private readonly List<(Func<TProperty, bool> Predicate, string Message)> _conditions = [];

    /// <summary>
    /// An optional condition that determines whether the validation rules should be applied to the request object.
    /// </summary>
    private Func<TRequest, bool>? _when;

    /// <summary>
    /// An optional child validator for the property, allowing for nested validation of complex types.
    /// </summary>
    private AbstractValidator<TProperty>? _childValidator;

    ///// <summary>
    ///// An optional child validator for a nullable property, allowing nested validation
    ///// where the value may be null (null values are skipped).
    ///// </summary>
    //private AbstractValidator<TProperty?>? _childValidatorNullable;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyRule{TRequest, TProperty}"/> class with the specified property name and getter function.
    /// </summary>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="getter">A function to get the value of the property from the request object.</param>
    public PropertyRule(string propertyName, Func<TRequest, TProperty> getter)
    {
        _propertyName = propertyName;
        _getter = getter;
    }

    /// <summary>
    /// Adds a validation condition to the rule, specifying a predicate that must be satisfied and an associated error message.
    /// </summary>
    /// <param name="predicate">A function that defines the condition to be met for the property value.</param>
    /// <param name="defaultMessage">The error message to be used if the condition is not met.</param>
    /// <returns>The current instance of the rule builder.</returns>
    public IRuleBuilder<TRequest, TProperty> Must(Func<TProperty, bool> predicate, string defaultMessage)
    {
        _conditions.Add((predicate, defaultMessage));
        return this;
    }

    /// <summary>
    /// Sets a custom error message for the most recently added validation condition. This method must be called after a condition has been added using the Must method.
    /// </summary>
    /// <param name="message">The custom error message to be used for the most recently added validation condition.</param>
    /// <returns>The current instance of the rule builder.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no validation condition has been added before calling this method.</exception>
    public IRuleBuilder<TRequest, TProperty> WithMessage(string message)
    {
        if (_conditions.Count == 0)
        {
            throw new InvalidOperationException(
                "WithMessage must follow a condition such as Must/NotEmpty/GreaterThan.");
        }

        var (predicate, _) = _conditions[^1];
        _conditions[^1] = (predicate, message);
        return this;
    }

    /// <summary>
    /// Specifies a condition that determines whether the validation rules should be applied to the request object. If the condition evaluates to false, the validation rules will be skipped for that request.
    /// </summary>
    /// <param name="condition">A function that defines the condition to be met for the validation rules to be applied.</param>
    /// <returns>The current instance of the rule builder.</returns>
    public IRuleBuilder<TRequest, TProperty> When(Func<TRequest, bool> condition)
    {
        _when = condition;
        return this;
    }

    /// <summary>
    /// Sets a child validator for the property, allowing for nested validation of complex types. 
    /// </summary>
    /// <param name="validator">The child validator to be used for the property.</param>
    /// <returns>The current instance of the rule builder.</returns>
    public IRuleBuilder<TRequest, TProperty> SetValidator(AbstractValidator<TProperty> validator)
    {
        _childValidator = validator;
        return this;
    }

    /// <summary>
    /// Validates the specified request object against the defined validation conditions for the property. If the optional "when" condition is set and evaluates to false, the validation will be skipped.
    /// </summary>
    /// <param name="instance">The request object to be validated.</param>
    /// <returns>A collection of validation errors, if any.</returns>
    public override IEnumerable<ValidationError> Validate(TRequest instance)
    {
        if (_when is not null && !_when(instance))
        {
            return [];
        }

        var value = _getter(instance);

        var errors = new List<ValidationError>();

        foreach (var (predicate, message) in _conditions)
        {
            if (!predicate(value))
            {
                errors.Add(new ValidationError(_propertyName, $"{_propertyName}: {message}"));
            }
        }

        if (_childValidator is not null && value is not null)
        {
            var childResult = _childValidator.Validate(value);
            foreach (var error in childResult.Errors)
            {
                errors.Add(new ValidationError($"{_propertyName}.{error.Title}", $"{_propertyName}.{error.Title}: {error.Description}"));
            }
        }

        return errors;
    }
}