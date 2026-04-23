using CourierHub.Abstractions.Models.Common;
using CourierHub.InPost.Client.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Responses;

/// <summary>
/// InPost specific response model for a successful parcel creation. Contains all details returned by the InPost API.
/// </summary>
internal sealed class InPostCreateParcelResponse
{
    /// <summary>
    /// The URI/href reference for this shipment resource in the InPost API.
    /// </summary>
    public required string Href { get; init; }

    /// <summary>
    /// The Identifier of the created shipment in the InPost system. This is a unique integer assigned by InPost to each shipment and can be used for tracking and reference purposes within the InPost API.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// The current status of the shipment in the InPost system.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// The tracking number of the shipment. This identifier is used in the logistics system for tracking the parcel's delivery progress.
    /// </summary>
    public string? TrackingNumber { get; init; }

    /// <summary>
    /// The return shipment tracking number. This value is only populated when the shipment is_return is true.
    /// </summary>
    public string? ReturnTrackingNumber { get; init; }

    /// <summary>
    /// The unique identifier of the application/organization that created this shipment.
    /// </summary>
    public int ApplicationId { get; init; }

    /// <summary>
    /// The ID of the user who created the shipment (if the user is logged in when the shipment was created).
    /// </summary>
    public int CreatedById { get; init; }

    /// <summary>
    /// Indicates whether the shipment has the "Paczka w Weekend" (Package on Weekend) service enabled.
    /// When true, the shipment can be picked up or delivered on weekends.
    /// </summary>
    public required bool EndOfWeekCollection { get; init; }

    /// <summary>
    /// The timestamp when the shipment was created in the InPost ShipX system.
    /// This is set automatically by the InPost API.
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// The timestamp when the shipment was last updated in the InPost ShipX.
    /// This is updated automatically by the InPost API whenever shipment details change.
    /// </summary>
    public required DateTime UpdatedAt { get; init; }
}