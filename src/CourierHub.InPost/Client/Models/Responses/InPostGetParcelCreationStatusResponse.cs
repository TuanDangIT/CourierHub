using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Responses;

internal sealed class InPostGetParcelCreationStatusResponse
{
    /// <summary>
    /// The Identifier of the shipment in the InPost system. 
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// The current status of the shipment in the InPost system.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// List of parcels included in this shipment. Each parcel contains its tracking number. A shipment can consist of one or more parcels.
    /// </summary>
    public required IEnumerable<InPostGetParcelCreationStatusResponseParcel> Parcels { get; init; }

}

/// <summary>
/// Parcel details returned in the response for parcel creation status. The parcel includes only tracking number.
/// </summary>
internal sealed class InPostGetParcelCreationStatusResponseParcel
{
    /// <summary>
    /// The tracking number of the shipment. This identifier is used in the logistics system for tracking the parcel's delivery progress.
    /// </summary>
    public required string TrackingNumber { get; init; }
}
