using CourierHub.Abstractions.Models.Common;
using CourierHub.InPost.Client.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Responses;

/// <summary>
/// InPost specific response model for a successful parcel creation. Contains all details returned by the InPost API.
/// </summary>
internal sealed class InPostCreateParcelResponse
{
    /// <summary>
    /// The URI/href reference for this shipment resource in the InPost API.
    /// </summary>
    public required string Href { get; init; }

    /// <summary>
    /// The Identifier of the created shipment in the InPost system. This is a unique integer assigned by InPost to each shipment and can be used for tracking and reference purposes within the InPost API.
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
    /// The return shipment tracking number. This value is provided only if package is returned to sender, meaning delivery failed.
    /// TODO: Veriy if it's needed.
    /// </summary>
    //public string? ReturnTrackingNumber { get; init; }

    /// <summary>
    /// The service code used for this shipment.
    /// </summary>
    public required string Service { get; init; }

    /// <summary>
    /// The reference string provided during shipment creation. This is an optional field that can be used to store any custom reference or identifier for the shipment, such as an order number or internal reference code.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// Is return shipment. Indicates whether this shipment is a return shipment. If true, the shipment is intended for returning items back to the sender or a specified return address. If false, it is a standard outbound shipment.
    /// </summary>
    public required bool IsReturn { get; init; }

    /// <summary>
    /// The unique identifier of the application/organization that created this shipment.
    /// </summary>
    public required int ApplicationId { get; init; }

    /// <summary>
    /// The created by id indetifier of the user who created this shipment. This is the ID of the user in the InPost system who initiated the creation of the shipment.
    /// </summary>
    public int? CreatedById { get; init; }

    /// <summary>
    /// External customer identifier for the shipment, if provided in the request.
    /// </summary>
    public string? ExternalCustomerId { get; init; }

    /// <summary>
    /// The sending method used for this shipment. 
    /// </summary>
    public string? SendingMethod { get; init; }

    /// <summary>
    /// Indicates whether the shipment has the "Paczka w Weekend" (Package on Weekend) service enabled.
    /// When true, the shipment can be picked up or delivered on weekends.
    /// </summary>
    public required bool EndOfWeekCollection { get; init; }

    /// <summary>
    /// Comments or additional information about the shipment. This field can contain any notes or remarks related to the shipment, such as special handling instructions or internal comments. It is an optional field and may be null if no comments were provided.
    /// </summary>
    public string? Comments { get; init; }

    /// <summary>
    /// Mpk is property used for setting place of charge for the shipment. It is an optional field that can be used to specify the location or code for charging the shipment.
    /// </summary>
    public string? Mpk { get; init; }
    
    /// <summary>
    /// A list of additional services associated with the shipment. This field can contain any extra services or options selected for the shipment.
    /// </summary>
    public IEnumerable<string> AdditionalServices { get; init; } = [];

    /// <summary>
    /// Custom attributes for the shipment, if applicable.
    /// </summary>
    public InPostCustomAttributes? CustomAttributes { get; init; }

    /// <summary>
    /// Cash on delivery details for the parcel, if applicable. 
    /// </summary>
    public InPostCashOnDelivery? Cod { get; init; }

    /// <summary>
    /// Insurance details for the parcel, if applicable. 
    /// </summary>
    public InPostInsurance? Insurance { get; init; }

    /// <summary>
    /// The party sending the parcel (shipper).
    /// </summary>
    public required InPostPeer Sender { get; init; }

    /// <summary>
    /// The party receiving the parcel (recipient).
    /// </summary>
    public required InPostPeer Receiver { get; init; }

    /// <summary>
    /// Selected offer for this shipment. This field contains the details of the offer that was selected for this shipment, including the service, carrier, price, and any additional services included in the offer. 
    /// </summary>
    public InPostOffer? SelectedOffer { get; init; }

    /// <summary>
    /// Offers returned by the InPost API for this shipment. This field contains a list of all offers that were returned by the InPost API for this shipment, including the details of each offer such as service, carrier, price, and availability. The caller can use this information to compare different offers and make informed decisions about which offer to select for the shipment.
    /// </summary>
    public IEnumerable<InPostOffer> Offers { get; init; } = [];

    /// <summary>
    /// Transactions related to this shipment. This field contains a list of all transactions that have occurred for this shipment.
    /// </summary>
    public IEnumerable<InPostTransaction> Transactions { get; init; } = [];

    /// <summary>
    /// Parcels that are associated with this shipment. This field contains a list of all parcels that are part of this shipment, including their dimensions, weight, and any other relevant details. 
    /// </summary>
    public IEnumerable<InPostCreateParcelResponseParcel> Parcels { get; init; } = [];

    /// <summary>
    /// The timestamp when the shipment was created in the InPost ShipX system.
    /// This is set automatically by the InPost API.
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// The timestamp when the shipment was last updated in the InPost ShipX.
    /// This is updated automatically by the InPost API whenever shipment details change.
    /// </summary>
    public required DateTime UpdatedAt { get; init; }
}

/// <summary>
/// Represents a parcel item returned in the InPost create parcel response.
/// </summary>
internal sealed class InPostCreateParcelResponseParcel
{
    /// <summary>
    /// The parcel identifier within the shipment response.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// The identifier of the parcel within the shipment.
    /// </summary>
    public required string IdentifyNumber { get; init; }

    /// <summary>
    /// The tracking number assigned to the parcel, if available.
    /// </summary>
    public string? TrackingNumber { get; init; }

    /// <summary>
    /// Indicates whether the parcel is non-standard.
    /// </summary>
    public required bool IsNonStandard { get; init; }

    /// <summary>
    /// The parcel template name, if one was used.
    /// </summary>
    public string? Template { get; init; }

    /// <summary>
    /// The parcel dimensions.
    /// </summary>
    public InPostDimension? Dimensions { get; init; }

    /// <summary>
    /// The parcel weight.
    /// </summary>
    public InPostWeight? Weight { get; init; }
}