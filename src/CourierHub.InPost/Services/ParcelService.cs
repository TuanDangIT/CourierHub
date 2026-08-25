using CourierHub.Core.Base;
using CourierHub.Core.Errors;
using CourierHub.Core.Result;
using CourierHub.InPost.Client;
using CourierHub.InPost.Client.Models.Requests;
using CourierHub.InPost.Client.Models.Responses;
using CourierHub.InPost.Client.Validators;
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

    public async Task<Result<CreateParcelResponse>> CreateParcelAsync(CreateParcelRequest request)
    {
        //try
        //{
            ArgumentNullException.ThrowIfNull(request);

            var validationResult = new CreateParcelRequestValidator().Validate(request);

            if (validationResult.IsFailure)
            {
                return Result.Failure<CreateParcelResponse>(validationResult.Errors);
            }

            var result = await _httpClient.CreateShipmentAsync(request);
            return result;
        //}
        //catch (Exception ex)
        //{
        //    _logger?.LogError(ex, "Failed to create parcel.");
        //    var error = new Error("ServerError", "Server error", "An unexpected error occurred while creating the parcel.");
        //    return Result.Failure<CreateParcelResponse>([error]);
        //}
    }

    public async Task<Result<CreateParcelBatchResponse>> CreateParcelBatchAsync(CreateParcelBatchRequest request)
    {
        try
        {
            var result = await _httpClient.CreateParcelBatchAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to create batch parcels.");
            var error = new Error("ServerError", "Server error", "An unexpected error occurred while creating batch parcels.");
            return Result.Failure<CreateParcelBatchResponse>([error]);
        }
    }

    public async Task<Result<PayForParcelResponse>> PayForParcelAsync(PayForParcelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var result = await _httpClient.PayForParcelAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to pay for parcel.");
            var error = new Error("ServerError", "Server error", "An unexpected error occurred while paying for the parcel.");
            return Result.Failure<PayForParcelResponse>([error]);
        }
    }

    public async Task<Result<byte[]>> GetLabelAsync(GetLabelRequest request)
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

    public async Task<Result<GetParcelsResponse>> GetParcelsAsync(GetParcelsRequest request)
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

    public async Task<Result<GetParcelBatchResponse>> GetBatchParcelsAsync(GetParcelBatchRequest request)
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
