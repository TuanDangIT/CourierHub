using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Client.Models.Common;

/// <summary>
/// DPD specific representation of a PUDO (Pick-Up Drop-Off) receiver, used in DPD API requests. 
/// </summary>
public sealed class PudoReceiver
{
    /// <summary>
    /// The name of the sender or receiver.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The email address.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The country code using ISO 3166-1 alpha-2 format (e.g., "PL", "DE", "FR").
    /// </summary>
    public required string CountryCode { get; init; }

    /// <summary>
    /// The phone number in any standard format (e.g., "+48123456789", "123456789").
    /// Normalization to courier-specific format is handled by individual providers.
    /// </summary>
    public required string Phone { get; init; }

    /// <summary>
    /// The name of the company or organization, if applicable.
    /// </summary>
    public string? CompanyName { get; init; }
}
