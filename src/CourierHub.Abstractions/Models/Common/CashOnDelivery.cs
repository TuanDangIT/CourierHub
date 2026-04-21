using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Common
{
    /// <summary>
    /// Represents the cash on delivery payment details, including the amount to be collected and the currency code.
    /// </summary>
    public record CashOnDelivery
    {
        /// <summary>
        /// Amount to be collected from the recipient upon delivery.
        /// </summary>
        public required decimal Amount { get; init; }

        /// <summary>
        /// Currency code for the cash on delivery amount, following ISO 4217 standard (e.g., "USD", "EUR", "PLN"). 
        /// </summary>
        /// <remarks>Defaults to "PLN" for Polish Złoty.</remarks>
        public required string Currency { get; init; } = "PLN";
    }
}
