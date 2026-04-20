using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents the physical dimensions of a parcel specific to InPost.
/// </summary>
public record InPostDimension
{
    /// <summary>
    /// The length of the parcel.
    /// </summary>
    public required decimal Length { get; init; }

    /// <summary>
    /// The width of the parcel.
    /// </summary>
    public required decimal Width { get; init; }

    /// <summary>
    /// The height of the parcel.
    /// </summary>
    public required decimal Height { get; init; }

    /// <summary>
    /// The unit of measurement (e.g., "mm", "cm", "m", "inches").
    /// </summary>
    public required string Unit { get; init; }

    /// <summary>
    /// The flag indicating whether the dimensions are non-standard (e.g., irregular shape, oversized).
    /// </summary>
    public bool? IsNonStandad { get; init; }
}
