using CourierHub.InPost.Client.Models.Request;
using CourierHub.InPost.Client.Models.Responses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CourierHub.InPost.Client;

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
