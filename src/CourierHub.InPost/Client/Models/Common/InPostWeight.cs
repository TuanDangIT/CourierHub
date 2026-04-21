using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents the weight of a parcel specific to InPost.
/// </summary>
internal sealed class InPostWeight
{
    /// <summary>
    /// The weight value.
    /// </summary>
    public required decimal Value { get; init; }

    /// <summary>
    /// The unit of measurement (e.g., "kg", "g", "lbs").
    /// </summary>
    public required string Unit { get; init; }
}
