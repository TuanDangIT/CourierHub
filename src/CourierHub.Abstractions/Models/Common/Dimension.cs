using System;

namespace CourierHub.Abstractions.Models.Common;

/// <summary>
/// Represents the physical dimensions of a parcel.
/// </summary>
public record Dimension
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
    /// The unit of measurement (e.g., "mm", "cm", "m").
    /// Defaults to "mm" (millimeters).
    /// </summary>
    public string Unit { get; init; } = "mm";

    /// <summary>
    /// The flag indicating whether the dimensions are non-standard (e.g., irregular shape, oversized). This is optional and may not be supported by all courier providers.
    /// </summary>
    public bool? IsNonStandad { get; init; }
}
