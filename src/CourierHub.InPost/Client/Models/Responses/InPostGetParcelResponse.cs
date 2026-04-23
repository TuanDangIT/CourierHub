using CourierHub.InPost.Client.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Responses
{
    internal sealed class InPostGetParcelResponse
    {
        /// <summary>
        /// The URI/href reference for this shipment resource in the InPost API.
        /// </summary>
        public required string Href { get; init; }

        /// <summary>
        /// The Identifier of the shipment in the InPost system. 
        /// </summary>
        public required int Id { get; init; }

        /// <summary>
        /// The current status of the shipment in the InPost system.
        /// </summary>
        public required string Status { get; init; }

        /// <summary>
        /// The tracking number of the shipment. This identifier is used in the logistics system for tracking the parcel's delivery progress.
        /// </summary>
        public string? TrackingNumber { get; init; }

        /// <summary>
        /// The return shipment tracking number. This value is only populated when the shipment is_return is true.
        /// </summary>
        public string? ReturnTrackingNumber { get; init; }

        /// <summary>
        /// The unique identifier of the application/organization that created this shipment.
        /// </summary>
        public int ApplicationId { get; init; }

        /// <summary>
        /// The ID of the user who created the shipment (if the user is logged in when the shipment was created).
        /// </summary>
        public int CreatedById { get; init; }

        /// <summary>
        /// Indicates whether the shipment has the "Paczka w Weekend" (Package on Weekend) service enabled.
        /// When true, the shipment can be picked up or delivered on weekends.
        /// </summary>
        public required bool EndOfWeekCollection { get; init; }

        /// <summary>
        /// The party sending the parcel (shipper).
        /// </summary>
        public required InPostPeer Sender { get; init; }

        /// <summary>
        /// The party receiving the parcel (recipient).
        /// </summary>
        public required InPostPeer Receiver { get; init; }

        /// <summary>
        /// The list of parcels to be shipped. Each parcel includes its dimensions and weight. A shipment can consist of one or more parcels.
        /// </summary>
        public required IEnumerable<InPostParcel> Parcels { get; init; }

        /// <summary>
        /// Insurance details for the parcel, if applicable. 
        /// </summary>
        public InPostInsurance? Insurance { get; init; }

        /// <summary>
        /// Cash on delivery details for the parcel, if applicable. 
        /// </summary>
        public InPostCashOnDelivery? Cod { get; init; }

        /// <summary>
        /// Service code or identifier for the courier service to be used.
        /// </summary>
        public required string Service { get; init; }

        /// <summary>
        /// Custom attributes for the shipment, if applicable.
        /// </summary>
        public InPostCustomAttributes? CustomAttributes { get; init; }

        /// <summary>
        /// Cost center - MPK for the shipment, if applicable. 
        /// </summary>
        public string? Mpk { get; init; }

        /// <summary>
        /// Reference or note for the shipment, if applicable. The property may be used for internal tracking or identification purposes.
        /// </summary>
        public string? Reference { get; init; }

        /// <summary>
        /// Is Return shipment. Indicates whether the parcel is being sent as a return to the sender. 
        /// </summary>
        public bool? IsReturn { get; init; }

        /// <summary>
        /// Additional comments, if applicable. 
        /// </summary>
        public string? Comments { get; init; }

        /// <summary>
        /// Additional services for the shipment ex. "email" or "sms", if applicable. This is optional.
        /// </summary>
        public IEnumerable<string>? AdditionalServices { get; init; }

        /// <summary>
        /// Created at timestamp for the shipment in the InPost ShipX system. This is set automatically by the InPost API when the shipment is created and indicates when the shipment record was created in the InPost system.
        /// </summary>
        public DateTime CreatedAt { get; init; }

        /// <summary>
        /// Updated at timestamp for the shipment in the InPost ShipX system. This is set automatically by the InPost API when the shipment is updated and indicates when the shipment record was last updated in the InPost system.
        /// </summary>
        public DateTime? UpdatedAt { get; init; }
    }
}
