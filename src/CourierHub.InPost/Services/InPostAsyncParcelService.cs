using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Interfaces;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using CourierHub.Core.Logging;
using CourierHub.InPost.Client;
using CourierHub.InPost.Client.Models.Extensions;
using CourierHub.InPost.Mappers;
using Microsoft.Extensions.Logging;

namespace CourierHub.InPost.Services;

internal sealed class InPostAsyncParcelService : IAsyncParcelService
{
    private readonly InPostHttpClient _httpClient;
    private readonly InPostMapper _mapper;
    private readonly ILogger? _logger;

    public InPostAsyncParcelService(InPostHttpClient httpClient, InPostMapper mapper, ILogger? logger = default)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new InPost shipment by mapping the unified request to InPost API contract,
    /// sending the request, and mapping the API response back to unified response model.
    /// </summary>
    /// <typeparam name="TExtension">The type of courier-specific response extension requested by the caller.</typeparam>
    /// <param name="request">Parcel creation request containing the details of the parcel to be created.</param>
    /// <returns>Create async parcel response with typed extension.</returns>
    public Task<CreateAsyncParcelResponse<TExtension>> CreateParcelAsync<TExtension>(CreateParcelRequest request)
        where TExtension : ICourierResponseExtension
    {
        ArgumentNullException.ThrowIfNull(request);

        return ExecuteLoggedAsync(nameof(CreateParcelAsync), async () =>
        {
            if (typeof(TExtension) != typeof(InPostCreateAsyncParcelResponseExtension))
            {
                throw new NotSupportedException($"InPost async parcel service supports extension type '{typeof(InPostCreateAsyncParcelResponseExtension).Name}' only.");
            }

            var inPostRequest = _mapper.MapToCreateParcelRequest(request);
            var inPostResponse = await _httpClient.CreateShipmentAsync(inPostRequest);
            var typedResponse = _mapper.MapToCreateParcelResponse(inPostResponse);

            return new CreateAsyncParcelResponse<TExtension>
            {
                ParcelId = typedResponse.ParcelId,
                Status = typedResponse.Status,
                TrackingNumbers = typedResponse.TrackingNumbers,
                Extension = typedResponse.Extension is null
                    ? default
                    : (TExtension)(ICourierResponseExtension)typedResponse.Extension
            };
        });
    }

    /// <summary>
    /// Gets the status of a parcel creation by its unique identifier. The method retrieves the parcel information from InPost API.
    /// </summary>
    /// <param name="parcelId">The unique identifier of the parcel. Cannot be null or empty.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="GetParcelCreationStatusResponse"/> result.</returns>
    public Task<GetParcelCreationStatusResponse> GetParcelCreationStatusAsync(string parcelId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(parcelId);

        return ExecuteLoggedAsync(nameof(GetParcelCreationStatusAsync), async () =>
        {
            var inPostResponse = await _httpClient.GetParcelCreationStatusAsync(parcelId);
            return _mapper.MapToGetParcelResponse(inPostResponse);
        });
    }

    /// <summary>
    /// Retrieves shipment label bytes for the given parcel identifier.
    /// </summary>
    /// <param name="request">The request containing the details for retrieving the label.</param>
    /// <returns>Byte array representing the shipment label in the specified format.</returns>
    public Task<byte[]> GetLabelAsync(GetLabelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return ExecuteLoggedAsync(nameof(GetLabelAsync), () => _httpClient.GetLabelAsync(request.ParcelId, request.Format, request.Type));
    }

    private async Task<T> ExecuteLoggedAsync<T>(string operationName, Func<Task<T>> action)
    {
        using var scope = _logger.BeginOperationScope(operationName, out var operationId);
        _logger.LogOperationStarted(operationName, operationId);

        try
        {
            var result = await action();
            _logger.LogOperationCompleted(operationName, operationId);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogOperationFailed(ex, operationName, operationId);
            throw;
        }
    }
}
