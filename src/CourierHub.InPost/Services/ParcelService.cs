using CourierHub.Core.Base;
using CourierHub.InPost.Client;
using CourierHub.InPost.Client.Models.Requests;
using CourierHub.InPost.Client.Models.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CourierHub.InPost.Services;

internal sealed class ParcelService : CourierServiceBase, IParcelService
{
    private readonly InPostHttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParcelService"/> class.
    /// </summary>
    /// <param name="httpClient">The InPost HTTP client used to call the API.</param>
    /// <param name="logger">The logger instance used for operation logging.</param>
    public ParcelService(InPostHttpClient httpClient, ILogger<ParcelService>? logger = default) : base(logger)
    {
        _httpClient = httpClient;
    }

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

    public async Task<CreateBatchParcelsResponse> CreateBatchParcelsAsync(CreateBatchParcelsRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var result = await _httpClient.CreateBatchParcelsAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to create batch parcels");
            throw;
        }
    }

    public async Task<PayForParcelResponse> PayForParcelAsync(PayForParcelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var result = await _httpClient.PayForParcelAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to pay for parcel");
            throw;
        }
    }

    public async Task<byte[]> GetLabelAsync(GetLabelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var result = await _httpClient.GetLabelAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to get label");
            throw;
        }
    }

    public async Task<GetParcelsResponse> GetParcelsAsync(GetParcelsRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var result = await _httpClient.GetParcelsAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to get parcels");
            throw;
        }
    }

    public async Task<GetBatchParcelsResponse> GetBatchParcelsAsync(GetBatchParcelsRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var result = await _httpClient.GetBatchParcelsAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to get batch parcels");
            throw;
        }
    }
}
