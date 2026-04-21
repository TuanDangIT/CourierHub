using System.Text.Json.Serialization;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Custom attributes for InPost shipments. This class encapsulates additional information that may be required by InPost for processing shipments, such as target point, sending method, and drop-off point. 
/// </summary>
internal sealed class InPostCustomAttributes
{
    /// <summary>
    /// Target point for the shipment. This is optional.
    /// </summary>
    public string? TargetPoint { get; init; }

    /// <summary>
    /// Sending method for the shipment. This is optional.
    /// </summary>
    public string? SendingMethod { get; init; }

    /// <summary>
    /// Drop off point for the shipment. This is optional.
    /// </summary>
    [JsonPropertyName("dropoff_point")]
    public string? DropOffPoint { get; init; }
}
