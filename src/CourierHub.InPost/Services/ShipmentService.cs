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

internal sealed class ShipmentService : CourierServiceBase, IShipmentService
{
    private readonly InPostHttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShipmentService"/> class.
    /// </summary>
    /// <param name="httpClient">The InPost HTTP client used to call the API.</param>
    /// <param name="logger">The logger instance used for operation logging.</param>
    public ShipmentService(InPostHttpClient httpClient, ILogger<ShipmentService>? logger = default) : base(logger)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<CreateShipmentResponse>> CreateShipmentAsync(CreateShipmentRequest request)
    {
        //try
        //{
            ArgumentNullException.ThrowIfNull(request);

            var validationResult = new CreateShipmentRequestValidator().Validate(request);

            if (validationResult.IsFailure)
            {
                return Result.Failure<CreateShipmentResponse>(validationResult.Errors);
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

    public async Task<Result<CreateShipmentBatchResponse>> CreateShipmentBatchAsync(CreateShipmentBatchRequest request)
    {
        try
        {
            var result = await _httpClient.CreateShipmentBatchAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to create batch shipments.");
            var error = new Error("ServerError", "Server error", "An unexpected error occurred while creating batch parcels.");
            return Result.Failure<CreateShipmentBatchResponse>([error]);
        }
    }

    public async Task<Result<PayForShipmentResponse>> PayForShipmentAsync(string shipmentId, PayForShipmentRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrEmpty(shipmentId);

        try
        {
            var result = await _httpClient.PayForShipmentAsync(shipmentId, request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to pay for parcel.");
            var error = new Error("ServerError", "Server error", "An unexpected error occurred while paying for the parcel.");
            return Result.Failure<PayForShipmentResponse>([error]);
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

    public async Task<Result<GetShipmentsResponse>> GetShipmentsAsync(GetShipmentsRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var result = await _httpClient.GetShipmentsAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to get shipments");
            throw;
        }
    }

    public async Task<Result<GetShipmentBatchResponse>> GetShipmentBatchAsync(GetShipmentBatchRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var result = await _httpClient.GetShipmentBatchAsync(request);
            return result;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to get batch shipments");
            throw;
        }
    }
}
