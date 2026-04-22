using CourierHub.Core.Base;
using CourierHub.Core.Configuration;
using CourierHub.InPost.Client.Models.Request;
using CourierHub.InPost.Client.Models.Responses;
using CourierHub.InPost.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CourierHub.InPost.Client
{
    internal sealed class InPostHttpClient : HttpClientBase
    {
        private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true
        };

        private readonly InPostOptions _inPostOptions;

        public InPostHttpClient(HttpClient httpClient, InPostOptions inPostOptions, HttpResilienceOptions? resilienceOptions = default, ILogger? logger = default)
            : base(httpClient, logger, resilienceOptions)
        {
            _inPostOptions = inPostOptions;

            _httpClient.BaseAddress = new Uri(_inPostOptions.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _inPostOptions.ApiKey);
        }

        /// <summary>
        /// Creates an InPost shipment and returns the normalized InPost response DTO.
        /// </summary>
        public async Task<InPostCreateParcelResponse> CreateShipmentAsync(InPostCreateParcelRequest shipment, CancellationToken cancellationToken = default)
        {
            var serializedShipment = JsonSerializer.Serialize(shipment, SerializerOptions);
            using var jsonContent = new StringContent(serializedShipment, Encoding.UTF8, "application/json");

            var endpoint = $"v1/organizations/{_inPostOptions.OrganizationId}/shipments";
            using var httpResponse = await _httpClient.PostAsync(endpoint, jsonContent, cancellationToken);
            var json = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorMessage = ExtractErrorMessage(json);
                throw new HttpRequestException($"HTTP request failed ({(int)httpResponse.StatusCode}): {errorMessage}");
            }

            var result = JsonSerializer.Deserialize<InPostCreateParcelResponse>(json, SerializerOptions);
            return result ?? throw new InvalidOperationException("InPost API returned an empty or invalid shipment response.");
        }

        /// <summary>
        /// Downloads shipment label bytes for the specified parcel identifier.
        /// </summary>
        public async Task<byte[]> GetLabelAsync(string parcelId, string format = "Pdf", CancellationToken cancellationToken = default)
        {
            var endpoint = $"v1/organizations/{_inPostOptions.OrganizationId}/shipments/{parcelId}/label?format={format}";
            using var httpResponse = await _httpClient.GetAsync(endpoint, cancellationToken);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                var errorMessage = ExtractErrorMessage(json);
                throw new HttpRequestException($"Label download failed ({(int)httpResponse.StatusCode}): {errorMessage}");
            }

            return await httpResponse.Content.ReadAsByteArrayAsync(cancellationToken);
        }

        private static string ExtractErrorMessage(string json)
        {
            try
            {
                using var errorJsonDocument = JsonDocument.Parse(json);
                var root = errorJsonDocument.RootElement;

                if (root.TryGetProperty("error", out var errorProperty) && errorProperty.ValueKind == JsonValueKind.String)
                {
                    return errorProperty.GetString() ?? "Unknown error.";
                }

                if (root.TryGetProperty("message", out var messageProperty) && messageProperty.ValueKind == JsonValueKind.String)
                {
                    return messageProperty.GetString() ?? "Unknown error.";
                }

                if (root.TryGetProperty("errors", out var errorsProperty) && errorsProperty.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in errorsProperty.EnumerateArray())
                    {
                        return item.ToString();
                    }
                }
            }
            catch
            {
                // Ignore parse failures and fallback to raw payload.
            }

            return string.IsNullOrWhiteSpace(json) ? "Unknown error." : json;
        }
    }
}
