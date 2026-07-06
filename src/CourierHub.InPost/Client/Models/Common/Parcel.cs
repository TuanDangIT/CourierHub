using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents the physical characteristics of a parcel specific to InPost.
/// </summary>
public class Parcel
{
    /// <summary>
    /// The physical dimensions of the parcel (length, width, height). InPost allows using template-based shipments where dimensions are determined by the template.
    /// </summary>
    public Dimension? Dimensions { get; init; }

    /// <summary>
    /// The weight of the parcel. This is optional as InPost allows using template-based shipments where weight is determined by the template.
    /// </summary>
    public Weight? Weight { get; init; }

    /// <summary>
    /// Template name or identifier for the parcel, if applicable. This is optional and may be used by InPost to apply predefined settings or rules to the parcel. The specific meaning and usage of this field are determined by InPost.
    /// </summary>
    public string? Template { get; init; }

    /// <summary>
    /// Indicates whether the parcel is non-standard.
    /// </summary>
    public bool? IsNonStandard { get; init; }
}
