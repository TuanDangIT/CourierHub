using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common.Responses;

/// <summary>
/// Represents a parcel item returned in the InPost payment response.
/// </summary>
public sealed class ParcelPaidResponse : Parcel
{
    /// <summary>
    /// Tracking number assigned to the parcel, if available.
    /// </summary>
    public string? TrackingNumber { get; init; }
}