using CourierHub.Dpd.Client.Models.Requests;
using CourierHub.Dpd.Client.Models.Responses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CourierHub.Dpd.Client;

/// <summary>
/// Dpd JSON context for source generation of JSON serialization and deserialization related to Dpd API interactions.
/// </summary>
[JsonSourceGenerationOptions(
    JsonSerializerDefaults.Web,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNameCaseInsensitive = true,
    GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(DpdCreateParcelRequest))]
[JsonSerializable(typeof(DpdCreateParcelResponse))]
internal partial class DpdJsonContext : JsonSerializerContext
{
}
