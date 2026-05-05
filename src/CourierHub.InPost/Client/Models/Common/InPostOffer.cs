using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common
{
    /// <summary>
    /// Represents an offer returned by the InPost API for a shipment.
    /// </summary>
    internal sealed class InPostOffer
    {
        /// <summary>
        /// The unique identifier of the offered service within the shipment.
        /// </summary>
        public required int Id { get; init; }

        /// <summary>
        /// The service details associated with this offer.
        /// </summary>
        public required InPostService Service { get; init; }

        /// <summary>
        /// The carrier details associated with this offer.
        /// </summary>
        public required InPostCarrier Carrier { get; init; }

        /// <summary>
        /// The additional services available for this offer.
        /// </summary>
        public IEnumerable<string> AdditionalServices { get; init; } = [];

        /// <summary>
        /// The current status of the offer.
        /// </summary>
        public required string Status { get; init; }

        /// <summary>
        /// The date and time until which the offer can be purchased.
        /// </summary>
        public DateTime? ExpiresAt { get; init; }

        /// <summary>
        /// The price of the offer.
        /// </summary>
        public decimal? Rate { get; init; }

        /// <summary>
        /// The currency used for the offer price.
        /// </summary>
        public required string Currency { get; init; }

        /// <summary>
        /// The reasons why the offer is unavailable, if any.
        /// </summary>
        public IEnumerable<string>? UnavailabilityReasons { get; init; }
    }
}
