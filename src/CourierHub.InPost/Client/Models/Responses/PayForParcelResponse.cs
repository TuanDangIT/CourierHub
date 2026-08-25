using System;
using System.Collections.Generic;
using CourierHub.InPost.Client.Models.Common;
using CourierHub.InPost.Client.Models.Common.Responses;

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
    public IReadOnlyList<ParcelPaidResponse> Parcels { get; init; } = [];

    /// <summary>
    /// Shipment custom attributes.
    /// </summary>
    public CustomAttributes? CustomAttributes { get; init; }

    /// <summary>
    /// Sender details.
    /// </summary>
    public required PeerResponse Sender { get; init; }

    /// <summary>
    /// Receiver details.
    /// </summary>
    public required PeerResponse Receiver { get; init; }

    /// <summary>
    /// Shipment creation timestamp.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Cash on delivery details for the shipment.
    /// </summary>
    public CashOnDeliveryResponse? CodAmount { get; init; }

    /// <summary>
    /// Insurance details for the shipment.
    /// </summary>
    public InsuranceResponse? Insurance { get; init; }

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
    /// Selected offers for the shipment.
    /// </summary>
    public IReadOnlyList<OfferSelectedResponse> Offers { get; init; } = [];

    /// <summary>
    /// Selected offer for the shipment.
    /// </summary>
    public OfferSelectedResponse? SelectedOffer { get; init; }

    /// <summary>
    /// Transactions associated with the shipment.
    /// </summary>
    public IReadOnlyList<Transaction> Transactions { get; init; } = [];
}