using CourierHub.Abstractions.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents the physical characteristics of a parcel specific to InPost.
/// </summary>
public record InPostParcel
{
    /// <summary>
    /// The physical dimensions of the parcel (length, width, height). This is optional as some courier providers allow using template-based shipments where dimensions are determined by the template.
    /// </summary>
    public Dimension? Dimension { get; init; }

    /// <summary>
    /// The weight of the parcel. This is optional as some courier providers allow using template-based shipments where weight is determined by the template.
    /// </summary>
    public Weight? Weight { get; init; }

    /// <summary>
    /// Template name or identifier for the parcel, if applicable. This is optional and may be used by some courier providers to apply predefined settings or rules to the parcel. The specific meaning and usage of this field are determined by each courier provider.
    /// </summary>
    public string? Template { get; init; }
}
