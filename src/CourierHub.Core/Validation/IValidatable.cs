using System.Collections.Generic;

namespace CourierHub.Core.Validation;

/// <summary>
/// Contract for objects that can validate themselves and return a list of validation errors.
/// </summary>
public interface IValidatable
{
    /// <summary>
    /// Validates the object and returns a list of validation errors. Returns an empty list when valid.
    /// </summary>
    /// <returns>List of validation errors.</returns>
    IReadOnlyList<ValidationError> Validate();
}
