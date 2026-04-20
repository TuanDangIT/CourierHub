using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents the recipient (receiver) of a parcel specific to InPost.
/// </summary>
public record InPostReceiver
{
    /// <summary>
    /// The recipient's first name.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// The recipient's last name (family name).
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// The recipient's email address.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The recipient's phone number in any standard format (e.g., "+48123456789", "123456789").
    /// Normalization to courier-specific format is handled by individual providers.
    /// </summary>
    public required string PhoneNumber { get; init; }

    /// <summary>
    /// The recipient's delivery address.
    /// </summary>
    public required Address Address { get; init; }

    /// <summary>
    /// The name of the recipient's company or organization, if applicable.
    /// </summary>
    public string? CompanyName { get; init; }
}