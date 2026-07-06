using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents insurance details for a parcel specific to InPost, including the insured amount and currency.
/// </summary>
public sealed class Insurance
{
    /// <summary>
    /// The insured amount for the parcel.
    /// </summary>
    public decimal? Amount { get; init; }

    /// <summary>
    /// Currency code for the insurance amount, following ISO 4217 standard (e.g., "USD", "EUR", "PLN"). 
    /// </summary>
    public string? Currency { get; init; }
}
