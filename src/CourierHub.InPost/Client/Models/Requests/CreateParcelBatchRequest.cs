using CourierHub.Core.Validation;
using CourierHub.InPost.Client.Models.Common;
using CourierHub.InPost.Client.Models.Common.Requests;
using System.Collections.Generic;

namespace CourierHub.InPost.Client.Models.Requests;

/// <summary>
/// InPost batch creation request model for creating multiple shipments at once.
/// </summary>
public sealed class CreateParcelBatchRequest
{
    /// <summary>
    /// Applies the chosen offer to all shipments in the batch without automatically paying them.
    /// </summary>
    public bool OnlyChoiceOfOffer { get; init; }

    /// <summary>
    /// Shipments to be created in the batch.
    /// </summary>
    public required IReadOnlyList<BatchShipmentRequest> Shipments { get; init; }
}