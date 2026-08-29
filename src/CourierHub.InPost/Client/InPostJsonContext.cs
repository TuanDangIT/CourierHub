using CourierHub.InPost.Client.Models.Requests;
using CourierHub.InPost.Client.Models.Responses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CourierHub.InPost.Client;

/// <summary>
/// InPost JSON context for source generation of JSON serialization and deserialization related to InPost API interactions.
/// </summary>
[JsonSourceGenerationOptions(
    JsonSerializerDefaults.Web,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNameCaseInsensitive = true,
    GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(CreateShipmentRequest))]
[JsonSerializable(typeof(CreateShipmentBatchRequest))]
[JsonSerializable(typeof(CreateShipmentResponse))]
[JsonSerializable(typeof(CreateShipmentBatchResponse))]
[JsonSerializable(typeof(GetShipmentsRequest))]
[JsonSerializable(typeof(GetShipmentsResponse))]
[JsonSerializable(typeof(GetShipmentBatchRequest))]
[JsonSerializable(typeof(GetShipmentBatchResponse))]
[JsonSerializable(typeof(PayForShipmentRequest))]
[JsonSerializable(typeof(PayForShipmentResponse))]
[JsonSerializable(typeof(ErrorResponse))]
internal partial class InPostJsonContext : JsonSerializerContext
{
}
