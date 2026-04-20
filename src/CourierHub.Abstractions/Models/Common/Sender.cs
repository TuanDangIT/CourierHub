using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Common;

/// <summary>
/// Represents the sender (shipper) of a parcel.
/// </summary>
public record Sender
{
    /// <summary>
    /// The sender's first name.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// The sender's last name (family name).
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// The sender's email address.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The sender's phone number in any standard format (e.g., "+48123456789", "123456789").
    /// Normalization to courier-specific format is handled by individual providers.
    /// </summary>
    public required string PhoneNumber { get; init; }

    /// <summary>
    /// The sender's address (return address or pickup location).
    /// </summary>
    public required Address Address { get; init; }

    /// <summary>
    /// The name of the sender's company or organization, if applicable.
    /// </summary>
    public string? CompanyName { get; init; }
}
