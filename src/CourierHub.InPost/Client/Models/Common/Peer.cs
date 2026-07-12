using CourierHub.Core.Result;
using CourierHub.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents a party (sender or receiver) specific to InPost API.
/// </summary>
public class Peer : IValidatable
{
    /// <summary>
    /// The Name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// The first name.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// The last name (family name).
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// The email address.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The phone number in any standard format (e.g., "+48123456789", "123456789").
    /// Normalization to courier-specific format is handled by individual providers.
    /// </summary>
    public required string Phone { get; init; }

    /// <summary>
    /// The address.
    /// </summary>
    public required Address Address { get; init; }

    /// <summary>
    /// The name of the company or organization, if applicable.
    /// </summary>
    public string? CompanyName { get; init; }

    /// <summary>
    /// Validates the Peer object and returns a list of validation errors.
    /// </summary>
    /// <returns>List of validation errors (empty if valid).</returns>
    public IReadOnlyList<ValidationError> Validate()
    {
        var errors = ValidationRules.Combine(
            FirstName.IsRequired(nameof(FirstName)),
            LastName.IsRequired(nameof(LastName)),
            Email.IsRequired(nameof(Email)),
            Phone.IsRequired(nameof(Phone))
        );

        // merge nested address errors
        var addressErrors = Address.Validate();
        if (addressErrors is not null && addressErrors.Count > 0)
            errors.AddRange(addressErrors);

        return errors;
    }
}
