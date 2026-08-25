using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common.Responses;

/// <summary>
/// Represents a parcel item returned in the InPost create parcel response.
/// </summary>
public sealed class ParcelResponse : Parcel
{
    /// <summary>
    /// The identifier of the parcel within the shipment.
    /// </summary>
    public required string IdentifyNumber { get; init; }

    /// <summary>
    /// The tracking number assigned to the parcel, if available.
    /// </summary>
    public string? TrackingNumber { get; init; }
}