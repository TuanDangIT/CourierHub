using CourierHub.Abstractions.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Responses
{
    /// <summary>
    /// Parcel details response model. 
    /// </summary>
    public record class GetParcelResponse
    {
        /// <summary>
        /// The unique identifier for the parcel assigned by the courier provider.
        /// </summary>
        /// <remarks>
        /// This ID is used for subsequent API calls to retrieve labels, cancel shipments,
        /// or update parcel information depending on courier provider. Some actions might be not allowed.
        /// </remarks>
        public required string ParcelId { get; init; }

        /// <summary>
        /// The tracking number for the shipment.
        /// </summary>
        /// <remarks>
        /// This number can be used for tracking the parcel's delivery status and is
        /// typically displayed on shipping labels and communicated to customers.
        /// </remarks>
        public required string TrackingNumber { get; init; }

        /// <summary>
        /// The name of the courier provider that processed this request.
        /// </summary>
        public required string CourierName { get; init; }

        /// <summary>
        /// The party sending the parcel (shipper).
        /// </summary>
        public required Sender Sender { get; init; }

        /// <summary>
        /// The party receiving the parcel (recipient).
        /// </summary>
        public required Receiver Receiver { get; init; }

        /// <summary>
        /// The list of parcels to be shipped. Each parcel includes its dimensions and weight. A shipment can consist of one or more parcels, depending on the courier provider's capabilities and the sender's needs.
        /// </summary>
        public required IEnumerable<Parcel> Parcels { get; init; }

        /// <summary>
        /// Insurance details for the parcel, if applicable. This is optional and may not be supported by all courier providers.
        /// </summary>
        public Insurance? Insurance { get; init; }

        /// <summary>
        /// Cash on delivery details for the parcel, if applicable. This is optional and may not be supported by all courier providers.
        /// </summary>
        public CashOnDelivery? CashOnDelivery { get; init; }

        /// <summary>
        /// Service code or identifier for the courier service to be used (e.g., "standard", "express", "same-day"). The specific values and their meanings are determined by each courier provider.
        /// </summary>
        public required string ServiceCode { get; init; }

        /// <summary>
        /// Pick up point code for pickup, if applicable. This is optional and may not be supported by all courier providers. The specific format and meaning of this code are determined by each courier provider.
        /// </summary>
        public string? PickupPointCode { get; init; }

        /// <summary>
        /// Drop-off point code for drop-off, if applicable. This is optional and may not be supported by all courier providers. The specific format and meaning of this code are determined by each courier provider.
        /// </summary>
        public string? DropoffPointCode { get; init; }

        /// <summary>
        /// Sending method or delivery option for the parcel. The specific values and their meanings are determined by each courier provider. This field is optional and may not be required by all courier providers.
        /// </summary>
        public string? SendingMethod { get; init; }

        /// <summary>
        /// Cost center or billing code for the shipment, if applicable. This is optional and may not be supported by all courier providers. The specific format and meaning of this code are determined by each courier provider and may be used for internal accounting or cost allocation purposes.
        /// </summary>
        public string? CostCenter { get; init; }

        /// <summary>
        /// Reference or note for the shipment, if applicable. The property may be used for internal tracking or identification purposes.
        /// </summary>
        public string? Reference { get; init; }

        /// <summary>
        /// Is Return shipment. Indicates whether the parcel is being sent as a return to the sender. This is optional and may not be supported by all courier providers. If true, it signifies that the parcel is intended to be returned to the sender rather than delivered to a recipient. The specific handling of return shipments is determined by each courier provider.
        /// </summary>
        public bool? IsReturn { get; init; }

        /// <summary>
        /// Additional comments, if applicable. This is optional and may not be supported by all courier providers.
        /// </summary>
        public string? Comments { get; init; }

        /// <summary>
        /// Additional services for the shipment ex. "email" or "sms", if applicable. This is optional.
        /// </summary>
        public IEnumerable<string>? AdditionalServices { get; init; }

        /// <summary>
        /// Created at timestamp for the shipment.
        /// </summary>
        public DateTime CreatedAt { get; init; }

        /// <summary>
        /// Updated at timestamp for the shipment.
        /// </summary>
        public DateTime? UpdatedAt { get; init; }

        /// <summary>
        /// Additional metadata returned by the courier provider.
        /// </summary>
        /// <remarks>
        /// Some courier providers may return additional information in the response
        /// that is relevant to the caller (e.g., estimated delivery date, special handling codes).
        /// </remarks>
        public Dictionary<string, object> Metadata { get; init; } = [];
    }
}
