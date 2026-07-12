using CourierHub.Core.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Client.Models.Errors;

/// <summary>
/// Represents a validation error for Dpd.
/// </summary>
/// <param name="Code">The error code (can be dynamic or constant).</param>
/// <param name="Title">The title of the validation error.</param>
/// <param name="Description">The validation message.</param>
public sealed record DpdError(string Code, string Title, string? Description = default)
    : Error(Code, Title, Description)
{
    private const string DefaultCode = "DpdError";

    /// <summary>
    /// Initializes a new instance of the <see cref="DpdError"/> class with a default error code.
    /// </summary>
    /// <param name="title">The title of the validation error.</param>
    /// <param name="description">The validation message.</param>
    public DpdError(string title, string? description = default)
        : this(DefaultCode, title, description)
    {
    }
}