using System;
using System.Collections.Generic;
using CourierHub.InPost.Client.Models.Common;

namespace CourierHub.InPost.Client.Models.Responses;

/// <summary>
/// InPost response model returned after buying an offer for a shipment.
/// </summary>
public sealed class PayForParcelResponse
{
    /// <summary>
    /// The URI/href reference for this shipment resource in the InPost API.
    /// </summary>
    public required string Href { get; init; }

    /// <summary>
    /// The identifier of the shipment in the InPost system.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Parcels included in the shipment.
    /// </summary>
    public IReadOnlyList<PayForParcelResponseParcel> Parcels { get; init; } = [];

    /// <summary>
    /// Shipment custom attributes.
    /// </summary>
    public CustomAttributes? CustomAttributes { get; init; }

    /// <summary>
    /// Sender details.
    /// </summary>
    public required GetParcelsResponsePeer Sender { get; init; }

    /// <summary>
    /// Receiver details.
    /// </summary>
    public required GetParcelsResponsePeer Receiver { get; init; }

    /// <summary>
    /// Shipment creation timestamp.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Cash on delivery details for the shipment.
    /// </summary>
    public CashOnDelivery? CodAmount { get; init; }

    /// <summary>
    /// Insurance details for the shipment.
    /// </summary>
    public Insurance? Insurance { get; init; }

    /// <summary>
    /// Reference value provided for the shipment.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// Indicates whether the shipment is a return shipment.
    /// </summary>
    public bool IsReturn { get; init; }

    /// <summary>
    /// Tracking number assigned to the shipment, if available.
    /// </summary>
    public string? TrackingNumber { get; init; }

    /// <summary>
    /// External customer identifier.
    /// </summary>
    public string? ExternalCustomerId { get; init; }

    /// <summary>
    /// Offers available for the shipment.
    /// </summary>
    public IReadOnlyList<PayForParcelResponseOffer> Offers { get; init; } = [];

    /// <summary>
    /// Selected offer for the shipment.
    /// </summary>
    public PayForParcelResponseOffer? SelectedOffer { get; init; }

    /// <summary>
    /// Transactions associated with the shipment.
    /// </summary>
    public IReadOnlyList<Transaction> Transactions { get; init; } = [];
}

/// <summary>
/// Represents a parcel item returned in the InPost payment response.
/// </summary>
public sealed class PayForParcelResponseParcel : Parcel
{
    /// <summary>
    /// Tracking number assigned to the parcel, if available.
    /// </summary>
    public string? TrackingNumber { get; init; }
}

/// <summary>
/// Represents an offer item returned in the InPost payment response.
/// </summary>
public sealed class PayForParcelResponseOffer
{
    /// <summary>
    /// Offer identifier.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Service details for the offer.
    /// </summary>
    public required Service Service { get; init; }

    /// <summary>
    /// Carrier details for the offer.
    /// </summary>
    public required Carrier Carrier { get; init; }

    /// <summary>
    /// Additional services included in the offer.
    /// </summary>
    public IReadOnlyList<string> AdditionalServices { get; init; } = [];

    /// <summary>
    /// Offer status.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// Expiration date of the offer.
    /// </summary>
    public DateTimeOffset? ValidTo { get; init; }

    /// <summary>
    /// Offer price.
    /// </summary>
    public decimal? Rate { get; init; }

    /// <summary>
    /// Currency code.
    /// </summary>
    public required string Currency { get; init; }

    /// <summary>
    /// Reasons why the offer is unavailable, if any.
    /// </summary>
    public IReadOnlyList<string> UnavailabilityReasons { get; init; } = [];
}
