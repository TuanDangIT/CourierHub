using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourierHub.Core.Validation;

/// <summary>
/// Represents an abstract base class for validating requests of type <typeparamref name="TRequest"/>.
/// </summary>
/// <typeparam name="TRequest">The type of the request to validate.</typeparam>
internal abstract class AbstractValidator<TRequest> 
{
    /// <summary>
    /// A list of validation rules for the request of type <typeparamref name="TRequest"/>.
    /// </summary>
    private readonly List<Func<TRequest, IEnumerable<ValidationError>>> _rules = [];

    /// <summary>
    /// Defines a validation rule for a property of type <typeparamref name="TProperty"/> in the request of type <typeparamref name="TRequest"/>.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property to validate.</typeparam>
    /// <param name="propertyName">The name of the property to validate.</param>
    /// <param name="getter">A function to get the property value from the request.</param>
    /// <returns>An <see cref="IRuleBuilder{TRequest, TProperty}"/> for defining validation rules.</returns>
    protected IRuleBuilder<TRequest, TProperty> RuleFor<TProperty>(string propertyName, Func<TRequest, TProperty> getter)
    {
        var rule = new PropertyRule<TRequest, TProperty>(propertyName, getter);
        _rules.Add(rule.Validate);
        return rule;
    }

    /// <summary>
    /// Validates the specified request of type <typeparamref name="TRequest"/> and returns a <see cref="Result.Result"/> indicating the validation outcome.
    /// </summary>
    /// <param name="request">The request of type <typeparamref name="TRequest"/> to validate.</param>
    /// <returns>A <see cref="Result.Result"/> indicating the validation outcome.</returns>
    public Result.Result Validate(TRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var errors = new List<ValidationError>();
        foreach (var rule in _rules)
        {
            errors.AddRange(rule(request));
        }
        
        if (errors.Count > 0)
        {
            return Result.Result.Failure(errors);
        }

        return Result.Result.Success();
    }
}