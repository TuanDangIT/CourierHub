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
[JsonSerializable(typeof(CreateParcelBatchRequest))]
[JsonSerializable(typeof(CreateParcelResponse))]
[JsonSerializable(typeof(CreateParcelBatchResponse))]
[JsonSerializable(typeof(GetParcelsRequest))]
[JsonSerializable(typeof(GetParcelsResponse))]
[JsonSerializable(typeof(GetParcelBatchRequest))]
[JsonSerializable(typeof(GetParcelBatchResponse))]
[JsonSerializable(typeof(PayForParcelRequest))]
[JsonSerializable(typeof(PayForParcelResponse))]
[JsonSerializable(typeof(ErrorResponse))]
internal partial class InPostJsonContext : JsonSerializerContext
{
}
