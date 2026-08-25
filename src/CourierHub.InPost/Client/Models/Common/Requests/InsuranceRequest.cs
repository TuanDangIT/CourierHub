using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common.Requests;

/// <summary>
/// Represents insurance details for a parcel specific to InPost, including the insured amount and currency.
/// </summary>
public sealed class InsuranceRequest
{
    /// <summary>
    /// The insured amount for the parcel.
    /// </summary>
    public required decimal Amount { get; init; }

    /// <summary>
    /// Currency code for the insurance amount, following ISO 4217 standard (e.g., "USD", "EUR", "PLN"). 
    /// </summary>
    public string? Currency { get; init; }
}
