using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourierHub.Core.Validation;

/// <summary>
/// Represents a base class for property validation rules, providing a mechanism to validate properties of an instance of type TRequest and return any validation errors encountered.
/// </summary>
/// <typeparam name="TRequest">The type of the request being validated.</typeparam>
internal abstract class PropertyRuleBase<TRequest>
{
    /// <summary>
    /// Validates the specified instance of type TRequest and returns a collection of validation errors encountered during the validation process.
    /// </summary>
    /// <param name="instance">The instance of type TRequest to be validated.</param>
    /// <returns>A collection of validation errors encountered during the validation process.</returns>
    public abstract IEnumerable<ValidationError> Validate(TRequest instance);
}
