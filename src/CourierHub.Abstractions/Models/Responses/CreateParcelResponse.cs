using CourierHub.Abstractions.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Responses;

/// <summary>
/// Represents the response from a successful parcel creation request.
/// </summary>
public record CreateParcelResponse
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
    public required IEnumerable<string> TrackingNumbers { get; init; }

    /// <summary>
    /// Additional metadata returned by the courier provider.
    /// </summary>
    /// <remarks>
    /// Some courier providers may return additional information in the response.
    /// </remarks>
    public Dictionary<string, object?> Metadata { get; init; } = [];
}
