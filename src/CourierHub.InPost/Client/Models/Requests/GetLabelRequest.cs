using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Requests;

/// <summary>
/// Get label request model for retrieving the shipping label of a parcel from InPost.
/// </summary>
public sealed class GetLabelRequest
{
    /// <summary>
    /// Shipment Id for which the label is being requested. This is a required field and should be the unique identifier of the shipment in the InPost system for which you want to retrieve the label.
    /// </summary>
    public required string ShipmentId { get; set; }

    /// <summary>
    /// Format of the label. This is a required field.
    /// </summary>
    /// <remarks>Default is Pdf.</remarks>
    public required string Format { get; set; } = "Pdf";

    /// <summary>
    /// Type of the label. This is a required field.
    /// </summary>
    /// <remarks>Default is normal.</remarks>
    public required string Type { get; set; } = "normal";
}
