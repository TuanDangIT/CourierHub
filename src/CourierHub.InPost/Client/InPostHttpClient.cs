using CourierHub.Abstractions.Enums;
using CourierHub.Core.Base;
using CourierHub.Core.Configuration;
using CourierHub.InPost.Client.Models.Request;
using CourierHub.InPost.Client.Models.Responses;
using CourierHub.InPost.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CourierHub.InPost.Client
{
    /// <summary>
    /// InPostHttpClient is a specialized HTTP client for interacting with the InPost API.
    /// </summary>
    internal sealed class InPostHttpClient : HttpClientBase
    {
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
        /// <param name="shipment">The shipment details to be created.</param>
        /// <param name="cancellationToken">Optional cancellation token for the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation, containing the InPostCreateParcelResponse DTO.</returns>
        public Task<InPostCreateParcelResponse> CreateShipmentAsync(InPostCreateParcelRequest shipment, CancellationToken cancellationToken = default)
        {
            var endpoint = $"v1/organizations/{_inPostOptions.OrganizationId}/shipments";
            return PostAsync(
                endpoint,
                shipment,
                InPostJsonContext.Default.InPostCreateParcelRequest,
                InPostJsonContext.Default.InPostCreateParcelResponse,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets shipment details for the specified parcel identifier.
        /// </summary>
        /// <param name="id">The identifier of the parcel to retrieve.</param>
        /// <param name="cancellationToken">Optional cancellation token for the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation, containing the InPostGetParcelResponse DTO.</returns>
        public Task<InPostGetParcelResponse> GetParcelAsync(string id, CancellationToken cancellationToken = default)
        {
            var endpoint = $"v1/organizations/{_inPostOptions.OrganizationId}/shipments?id={id}";
            return GetAsync(endpoint, InPostJsonContext.Default.InPostGetParcelResponse, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Downloads shipment label bytes for the specified parcel identifier.
        /// </summary>
        /// <param name="parcelId">The identifier of the parcel for which to download the label.</param>
        /// <param name="format">The desired format of the label (e.g., PDF, ZPL, ELP). Default is PDF.</param>
        /// <param name="cancellationToken">Optional cancellation token for the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation, containing the label bytes.</returns>
        public Task<byte[]> GetLabelAsync(string parcelId, LabelFormat format = LabelFormat.Pdf, CancellationToken cancellationToken = default)
        {
            var endpoint = $"v1/organizations/{_inPostOptions.OrganizationId}/shipments/{parcelId}/label?format={format}";
            return GetAsync(endpoint, cancellationToken: cancellationToken);
        }
    }
}
