using CourierHub.Core.Base;
using CourierHub.Core.Errors;
using CourierHub.Core.Result;
using CourierHub.Dpd.Client.Models.Errors;
using CourierHub.Dpd.Client.Models.Requests;
using CourierHub.Dpd.Client.Models.Responses;
using CourierHub.Dpd.Configurations;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

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
    /// <param name="logger">Optional logger for logging HTTP client operations.</param>
    public DpdHttpClient(HttpClient httpClient, DpdOptions dpdOptions, ILogger? logger = default)
        : base(httpClient, logger)
    {
        ArgumentNullException.ThrowIfNull(dpdOptions);

        _dpdOptions = dpdOptions;

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
    public Task<Result<CreateParcelResponse>> CreateShipmentAsync(CreateParcelRequest shipment, CancellationToken cancellationToken = default)
    {
        var endpoint = "public/shipment/v1/generatePackagesNumbers";

        return PostAsync(
            endpoint,
            shipment,
            DpdJsonContext.Default.CreateParcelRequest,
            DpdJsonContext.Default.CreateParcelResponse,
            MapDpdErrors,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Maps DPD error responses to a list of <see cref="Error"/> objects based on the content and status code.
    /// </summary>
    /// <param name="errorContent">The content of the error response.</param>
    /// <returns>A list of <see cref="Error"/> objects representing the errors.</returns>
    private static IReadOnlyList<Error> MapDpdErrors(string errorContent)
    {
        var trimmed = errorContent.TrimStart();

        if (trimmed.StartsWith('<'))
        {
            return MapDpdXmlErrors(errorContent);
        }

        return MapDpdJsonErrors(errorContent);
    }

    /// <summary>
    /// Maps DPD JSON error responses to a list of <see cref="Error"/> objects based on the content and status code.
    /// </summary>
    /// <param name="errorContent">The content of the error response.</param>
    /// <returns>A list of <see cref="Error"/> objects representing the errors.</returns>
    private static IReadOnlyList<Error> MapDpdJsonErrors(string errorContent)
    {
        using var document = JsonDocument.Parse(errorContent);

        return [new DpdError("status", document.RootElement.GetProperty("status").GetString()!)];
    }

    /// <summary>
    /// Maps DPD XML error responses to a list of <see cref="Error"/> objects based on the content and status code.
    /// </summary>
    /// <param name="errorContent">The content of the error response.</param>
    /// <returns>A list of <see cref="Error"/> objects representing the errors.</returns>
    private static IReadOnlyList<Error> MapDpdXmlErrors(string errorContent)
    {
        var document = XDocument.Parse(errorContent);

        var items = document
            .Descendants("errors")
            .Where(x => x.Element("userMessage") is not null || x.Element("field") is not null);

        var errors = new List<Error>();

        foreach (var item in items)
        {
            var field = item.Element("field")?.Value?.Trim();
            var message = item.Element("userMessage")?.Value?.Trim();

            errors.Add(new Error("DpdValidationError", field, message));
        }

        return errors;
    }
}
