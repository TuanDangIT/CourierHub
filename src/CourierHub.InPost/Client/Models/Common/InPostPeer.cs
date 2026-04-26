using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents a party (sender or receiver) specific to InPost API.
/// </summary>
internal sealed class InPostPeer
{
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
    public required InPostAddress Address { get; init; }

    /// <summary>
    /// The name of the company or organization, if applicable.
    /// </summary>
    public string? CompanyName { get; init; }
}
