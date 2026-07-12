using System;
using System.Collections.Generic;

namespace CourierHub.Core.Validation;

/// <summary>
/// Utility helpers for common validation checks.
/// </summary>
public static class ValidationRules
{
    /// <summary>
    /// Validates that a string contains a non-whitespace value.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A validation error when validation fails; otherwise null.</returns>
    public static ValidationError? IsRequired(this string? value, string fieldName)
        => string.IsNullOrWhiteSpace(value)
            ? new ValidationError(fieldName, $"{fieldName} is required.")
            : null;

    /// <summary>
    /// Validates that a string has at least the specified minimum length.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="minLength">The minimum allowed length.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A validation error when validation fails; otherwise null.</returns>
    public static ValidationError? HasMinLength(this string? value, int minLength, string fieldName)
        => value is null || value.Length < minLength
            ? new ValidationError(fieldName, $"{fieldName} must be at least {minLength} characters long.")
            : null;

    /// <summary>
    /// Validates that a string has at most the specified maximum length.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="maxLength">The maximum allowed length.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A validation error when validation fails; otherwise null.</returns>
    public static ValidationError? HasMaxLength(this string? value, int maxLength, string fieldName)
        => value is not null && value.Length > maxLength
            ? new ValidationError(fieldName, $"{fieldName} must be at most {maxLength} characters long.")
            : null;

    /// <summary>
    /// Validates that a string length falls within the specified inclusive range.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="minLength">The minimum allowed length.</param>
    /// <param name="maxLength">The maximum allowed length.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A validation error when validation fails; otherwise null.</returns>
    public static ValidationError? HasLength(this string? value, int minLength, int maxLength, string fieldName)
        => value is null || value.Length < minLength || value.Length > maxLength
            ? new ValidationError(fieldName, $"{fieldName} must be between {minLength} and {maxLength} characters long.")
            : null;

    /// <summary>
    /// Validates that a string has the specified length.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="length">The exact length to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A validation error when validation fails; otherwise null.</returns>
    public static ValidationError? HasLength(this string? value, int length, string fieldName)
        => value is null || value.Length != length
            ? new ValidationError(fieldName, $"{fieldName} must be exactly {length} characters long.")
            : null;

    /// <summary>
    /// Helper to combine multiple validation checks into a list of errors.
    /// Accepts null-returning checks and returns non-null errors in a list.
    /// </summary>
    /// <param name="checks">Validation checks that return ValidationError?.</param>
    /// <returns>List of validation errors (empty if none).</returns>
    public static List<ValidationError> Combine(params ValidationError?[] checks)
    {
        var list = new List<ValidationError>();
        foreach (var check in checks)
        {
            if (check is not null)
                list.Add(check);
        }
        return list;
    }
}
