using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common.Responses;

/// <summary>
/// Represents a single shipment item returned in InPost shipments search/list responses.
/// </summary>
public sealed class ShipmentListItemResponse
{
    /// <summary>
    /// The URI/href reference for the shipment resource in the InPost API.
    /// </summary>
    public required string Href { get; init; }

    /// <summary>
    /// The identifier of the shipment in the InPost system.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// The current shipment status.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// The shipment tracking number.
    /// </summary>
    public string? TrackingNumber { get; init; }

    /// <summary>
    /// The return shipment tracking number.
    /// </summary>
    public string? ReturnTrackingNumber { get; init; }

    /// <summary>
    /// The InPost service code used for this shipment.
    /// </summary>
    public required string Service { get; init; }

    /// <summary>
    /// The external reference provided for the shipment.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// Indicates whether this shipment is a return shipment.
    /// </summary>
    public bool IsReturn { get; init; }

    /// <summary>
    /// The identifier of the application/organization that created the shipment.
    /// </summary>
    public int ApplicationId { get; init; }

    /// <summary>
    /// The identifier of the user who created the shipment.
    /// </summary>
    public int? CreatedById { get; init; }

    /// <summary>
    /// External customer identifier.
    /// </summary>
    public string? ExternalCustomerId { get; init; }

    /// <summary>
    /// The shipment sending method.
    /// </summary>
    public string? SendingMethod { get; init; }

    /// <summary>
    /// Indicates whether end-of-week collection is enabled for the shipment.
    /// </summary>
    public bool EndOfWeekCollection { get; init; }

    /// <summary>
    /// Additional comments for the shipment.
    /// </summary>
    public string? Comments { get; init; }

    /// <summary>
    /// Cost center value (MPK) for the shipment.
    /// </summary>
    public string? Mpk { get; init; }

    /// <summary>
    /// List of additional services enabled for the shipment.
    /// </summary>
    public IReadOnlyList<string> AdditionalServices { get; init; } = [];

    /// <summary>
    /// Shipment custom attributes.
    /// </summary>
    public CustomAttributes? CustomAttributes { get; init; }

    /// <summary>
    /// Cash on delivery details for the shipment.
    /// </summary>
    public CashOnDeliveryResponse? Cod { get; init; }

    /// <summary>
    /// Insurance details for the shipment.
    /// </summary>
    public InsuranceResponse? Insurance { get; init; }

    /// <summary>
    /// Sender details.
    /// </summary>
    public required PeerResponse Sender { get; init; }

    /// <summary>
    /// Receiver details.
    /// </summary>
    public required PeerResponse Receiver { get; init; }

    /// <summary>
    /// The selected shipment offer, if one has been selected.
    /// </summary>
    public Offer? SelectedOffer { get; init; }

    /// <summary>
    /// Available offers returned for the shipment.
    /// </summary>
    public IReadOnlyList<Offer> Offers { get; init; } = [];

    /// <summary>
    /// Transactions associated with the shipment.
    /// </summary>
    public IReadOnlyList<Transaction> Transactions { get; init; } = [];

    /// <summary>
    /// Parcels included in the shipment.
    /// </summary>
    public IReadOnlyList<ParcelListResponse> Parcels { get; init; } = [];

    /// <summary>
    /// Shipment creation timestamp.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Shipment last update timestamp.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
}