using CourierHub.InPost.Client.Models.Request;
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
[JsonSerializable(typeof(InPostCreateParcelRequest))]
[JsonSerializable(typeof(InPostCreateParcelResponse))]
[JsonSerializable(typeof(InPostGetParcelResponse))]
internal partial class InPostJsonContext : JsonSerializerContext
{
}
