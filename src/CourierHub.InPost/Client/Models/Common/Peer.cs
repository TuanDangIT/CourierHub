using CourierHub.Core.Result;
using CourierHub.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents a party (sender or receiver) specific to InPost API.
/// </summary>
public class Peer
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

    ///// <summary>
    ///// Validates the Peer object and returns a list of validation errors.
    ///// </summary>
    ///// <returns>List of validation errors (empty if valid).</returns>
    //public IReadOnlyList<ValidationError> Validate()
    //{
    //    var errors = ValidationRules.Combine(
    //        ValidationRules.For(Name, nameof(Name)).HasMaxLength(255),
    //        ValidationRules.For(FirstName, nameof(FirstName)).IsRequired().HasMaxLength(255),
    //        ValidationRules.For(LastName, nameof(LastName)).IsRequired().HasMaxLength(255),
    //        ValidationRules.For(Email, nameof(Email)).IsRequired().HasMaxLength(255),
    //        ValidationRules.For(Phone, nameof(Phone)).IsRequired().HasMaxLength(255),
    //        ValidationRules.For(CompanyName, nameof(CompanyName)).HasMaxLength(255)
    //    );

    //    errors.AddRange(Address.Validate());

    //    return errors;
    //}
}
