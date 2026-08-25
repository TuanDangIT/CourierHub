using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common.Responses;

/// <summary>
/// Represents a single parcel item in InPost shipments search/list response items.
/// </summary>
public sealed class ParcelListResponse : Parcel
{
    /// <summary>
    /// InPost identifier of the parcel.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// The identifier of the parcel within the shipment.
    /// </summary>
    public required string IdentifyNumber { get; init; }

    /// <summary>
    /// The tracking number assigned to the parcel, if available.
    /// </summary>
    public string? TrackingNumber { get; init; }
}
