using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common.Responses;

/// <summary>
/// Represents the response model for a shipment in the InPost API, including its identifier, status, and tracking number.
/// </summary>
public sealed class ShipmentResponse
{
    /// <summary>
    /// The URI/href reference for the shipment resource in the InPost API.
    /// </summary>
    public required string Href { get; init; }

    /// <summary>
    /// The identifier of the shipment in the InPost system.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// The current shipment status.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// The shipment tracking number.
    /// </summary>
    public string? TrackingNumber { get; init; }
}