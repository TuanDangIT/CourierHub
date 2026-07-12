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
[JsonSerializable(typeof(CreateParcelRequest))]
[JsonSerializable(typeof(CreateBatchParcelsRequest))]
[JsonSerializable(typeof(CreateBatchParcelsRequestShipment))]
[JsonSerializable(typeof(CreateParcelResponse))]
[JsonSerializable(typeof(CreateBatchParcelsResponse))]
[JsonSerializable(typeof(CreateBatchParcelsResponseShipment))]
[JsonSerializable(typeof(GetParcelsRequest))]
[JsonSerializable(typeof(GetParcelsResponse))]
[JsonSerializable(typeof(GetBatchParcelsRequest))]
[JsonSerializable(typeof(GetBatchParcelsResponse))]
[JsonSerializable(typeof(PayForParcelRequest))]
[JsonSerializable(typeof(PayForParcelResponse))]
[JsonSerializable(typeof(PayForParcelResponseParcel))]
[JsonSerializable(typeof(PayForParcelResponseOffer))]
[JsonSerializable(typeof(ErrorResponse))]
internal partial class InPostJsonContext : JsonSerializerContext
{
}
