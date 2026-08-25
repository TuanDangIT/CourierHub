using CourierHub.Core.Errors;

namespace CourierHub.Core.Validation;

/// <summary>
/// Represents a validation error for a specific property.
/// </summary>
/// <param name="PropertyPath">The property path that failed validation.</param>
/// <param name="Description">The validation message.</param>
public sealed record ValidationError(string PropertyPath, string? Description = default)
    : Error("ValidationError", PropertyPath, Description);