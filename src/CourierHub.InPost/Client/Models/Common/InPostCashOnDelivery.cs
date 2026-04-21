using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents cash on delivery (COD) details for a parcel specific to InPost, including the amount to be collected and the currency code.
/// </summary>
internal sealed class InPostCashOnDelivery
{
    /// <summary>
    /// Amount to be collected from the recipient upon delivery.
    /// </summary>
    public required decimal Amount { get; init; }

    /// <summary>
    /// Currency code for the cash on delivery amount, following ISO 4217 standard (e.g., "USD", "EUR", "PLN"). 
    /// </summary>
    public required string Currency { get; init; }
}
