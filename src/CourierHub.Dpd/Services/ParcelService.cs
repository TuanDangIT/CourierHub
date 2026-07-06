using CourierHub.Core.Base;
using CourierHub.Dpd.Client;
using CourierHub.Dpd.Client.Models.Requests;
using CourierHub.Dpd.Client.Models.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CourierHub.Dpd.Services;

/// <summary>
/// DPD parcel service implementation.
/// </summary>
internal sealed class ParcelService : CourierServiceBase, IParcelService
{
    private readonly DpdHttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParcelService"/> class.
    /// </summary>
    /// <param name="httpClient">The DPD HTTP client used to call the API.</param>
    /// <param name="logger">The logger instance used for operation logging.</param>
    public ParcelService(DpdHttpClient httpClient, ILogger? logger = default)
        : base(logger)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Creates a DPD shipment.
    /// </summary>
    /// <param name="request">The parcel creation request.</param>
    /// <returns>The created parcel response.</returns>
    public async Task<CreateParcelResponse> CreateParcelAsync(CreateParcelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var result = await _httpClient.CreateShipmentAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to create parcel");
            throw;
        }
    }

    /// <summary>
    /// Retrieves the shipment label bytes.
    /// </summary>
    /// <param name="request">The request containing the parcel identifier and label settings.</param>
    /// <returns>The label bytes.</returns>
    public async Task<byte[]> GetLabelAsync(GetLabelRequest request)
    {
        throw new NotImplementedException();
        //ArgumentNullException.ThrowIfNull(request);

        //try
        //{
        //    var result = await _httpClient.GetLabelAsync(request);
        //    return result;
        //}
        //catch (Exception ex)
        //{
        //    _logger?.LogError(ex, "Failed to get label");
        //    throw;
        //}
    }
}
