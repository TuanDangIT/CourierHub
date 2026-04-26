using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Common;

/// <summary>
/// Represents insurance details for a parcel, including the insured amount and currency.
/// </summary>
public record Insurance
{
    /// <summary>
    /// The insured amount for the parcel.
    /// </summary>
    public decimal? Amount { get; init; }

    /// <summary>
    /// Currency code for the insurance amount, following ISO 4217 standard (e.g., "USD", "EUR", "PLN"). 
    /// </summary>
    /// <remarks>Defaults to "PLN" for Polish Złoty.</remarks>
    public string? Currency { get; init; } = "PLN";
}
