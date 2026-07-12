using System;
using System.Collections.Generic;
using System.Text;
using Result = CourierHub.Core.Result.Result;

namespace CourierHub.Core.Errors;

/// <summary>
/// Represents an error with a code and an optional description.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Title">The optional error title.</param>
/// <param name="Description">The optional error description.</param>
public record Error(string Code, string? Title = default, string? Description = default)
{
    /// <summary>
    /// Represents a successful operation with no error.
    /// </summary>
    public static readonly Error None = new(string.Empty);
}