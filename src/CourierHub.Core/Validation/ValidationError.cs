using CourierHub.Core.Errors;

namespace CourierHub.Core.Validation;

/// <summary>
/// Represents a validation error for a specific field.
/// </summary>
/// <param name="FieldName">The field that failed validation.</param>
/// <param name="Description">The validation message.</param>
public sealed record ValidationError(string FieldName, string? Description = default)
    : Error("ValidationError", FieldName, Description);
