using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Common;

/// <summary>
/// Represents the weight of a parcel.
/// </summary>
public record Weight
{
    /// <summary>
    /// The weight value.
    /// </summary>
    public required decimal Amount { get; init; }

    /// <summary>
    /// The unit of measurement (e.g., "kg", "g", "lbs").
    /// </summary>
    /// <remarks>Defaults to "kg" (kilograms).</remarks>
    public string Unit { get; init; } = "kg";
}
