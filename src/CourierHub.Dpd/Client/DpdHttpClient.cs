using CourierHub.Core.Base;
using CourierHub.Core.Configuration;
using CourierHub.Dpd.Client.Models.Requests;
using CourierHub.Dpd.Client.Models.Responses;
using CourierHub.Dpd.Configurations;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace CourierHub.Dpd.Client;

/// <summary>
/// DPD HTTP client for interacting with the DPD API.
/// </summary>
internal sealed class DpdHttpClient : HttpClientBase
{
    private readonly DpdOptions _dpdOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="DpdHttpClient"/> class with the specified dependencies.
    /// </summary>
    /// <param name="httpClient">The HTTP client used for making requests to the DPD API.</param>
    /// <param name="dpdOptions">The DPD options containing API credentials and configuration settings.</param>
    /// <param name="resilienceOptions">Optional resilience options for handling transient faults.</param>
    /// <param name="logger">Optional logger for logging HTTP client operations.</param>
    public DpdHttpClient(HttpClient httpClient, DpdOptions dpdOptions, HttpResilienceOptions? resilienceOptions = default, ILogger? logger = default)
        : base(httpClient, logger, resilienceOptions)
    {
        _dpdOptions = dpdOptions;

        _httpClient.BaseAddress = new Uri(_dpdOptions.BaseUrl);

        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_dpdOptions.Login}:{_dpdOptions.Password}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        _httpClient.DefaultRequestHeaders.Add("x-dpd-fid", _dpdOptions.MasterFID);
    }

    /// <summary>
    /// Creates a DPD shipment and returns the normalized DPD response DTO.
    /// </summary>
    /// <param name="shipment">The shipment details to be created.</param>
    /// <param name="cancellationToken">Optional cancellation token for the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the DpdCreateParcelResponse DTO.</returns>
    public Task<DpdCreateParcelResponse> CreateShipmentAsync(DpdCreateParcelRequest shipment, CancellationToken cancellationToken = default)
    {
        var endpoint = "public/shipment/v1/generatePackagesNumbers";

        return PostAsync(
            endpoint,
            shipment,
            DpdJsonContext.Default.DpdCreateParcelRequest,
            DpdJsonContext.Default.DpdCreateParcelResponse,
            cancellationToken: cancellationToken);
    }
}
