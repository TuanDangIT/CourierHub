using System.Text.Json.Serialization;

namespace CourierHub.InPost.Client.Models.Requests;

/// <summary>
/// InPost request model for paying for an existing shipment by selecting one of its available offers.
/// </summary>
public sealed class PayForParcelRequest
{
    /// <summary>
    /// Identifier of the shipment to be paid for.
    /// </summary>
    [JsonIgnore]
    public required string ShipmentId { get; init; }

    /// <summary>
    /// Identifier of the offer to purchase.
    /// </summary>
    public required int OfferId { get; init; }
}
