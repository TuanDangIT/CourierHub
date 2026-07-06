using System;
using System.Collections.Generic;

namespace CourierHub.InPost.Client.Models.Responses;

/// <summary>
/// InPost batch lookup response model.
/// </summary>
public sealed class GetBatchParcelsResponse
{
    /// <summary>
    /// The URI/href reference for this batch resource in the InPost API.
    /// </summary>
    public required string Href { get; init; }

    /// <summary>
    /// The identifier of the batch in the InPost system.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// The current status of the batch processing operation.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// Shipments associated with the batch.
    /// </summary>
    public IReadOnlyList<GetBatchParcelsResponseShipment> Shipments { get; init; } = [];

    /// <summary>
    /// Batch creation timestamp.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Batch last update timestamp.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>
/// Represents a shipment summary returned in batch lookup responses.
/// </summary>
public sealed class GetBatchParcelsResponseShipment
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
}
