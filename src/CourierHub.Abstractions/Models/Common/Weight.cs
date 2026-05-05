using System;
using System.Collections.Generic;
using System.Text;
using CourierHub.Abstractions.Enums;

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
    /// The unit of measurement.
    /// </summary>
    /// <remarks>Defaults to Kilogram (Kg).</remarks>
    public WeightUnit Unit { get; init; } = WeightUnit.Kg;
}
