using CourierHub.Abstractions.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Responses;

/// <summary>
/// Parcel creation status response model. 
/// </summary>
public record class GetParcelCreationStatusResponse
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
    /// Gets the current status of a parcel. Based on the status value, the caller can determine whether the parcel creation was successful and if the label is ready for retrieval. The specific status values and their meanings may vary depending on the courier provider's API contract.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// The tracking number for the shipment.
    /// </summary>
    /// <remarks>
    /// This number can be used for tracking the parcel's delivery status and is
    /// typically displayed on shipping labels and communicated to customers.
    /// </remarks>
    public required IEnumerable<string> TrackingNumbers { get; init; }

    /// <summary>
    /// Additional metadata returned by the courier provider.
    /// </summary>
    /// <remarks>
    /// Some courier providers may return additional information in the response.
    /// </remarks>
    public Dictionary<string, string?> Metadata { get; init; } = [];
}
