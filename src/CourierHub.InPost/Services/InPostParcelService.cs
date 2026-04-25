using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Interfaces;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using CourierHub.Core.Logging;
using CourierHub.InPost.Client;
using CourierHub.InPost.Mappers;
using Microsoft.Extensions.Logging;

namespace CourierHub.InPost.Services;

internal sealed class InPostParcelService : IParcelService
{
    private readonly InPostHttpClient _httpClient;
    private readonly InPostMapper _mapper;
    private readonly ILogger? _logger;

    public InPostParcelService(InPostHttpClient httpClient, InPostMapper mapper, ILogger? logger = default)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new InPost shipment by mapping the unified request to InPost API contract,
    /// sending the request, and mapping the API response back to unified response model.
    /// </summary>
    /// <param name="request">Parcel creation request containing the details of the parcel to be created.</param>
    /// <returns>Create parcel response.</returns>
    public Task<CreateParcelResponse> CreateParcelAsync(CreateParcelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return ExecuteLoggedAsync(nameof(CreateParcelAsync), async () =>
        {
            var inPostRequest = _mapper.MapToCreateParcelRequest(request);
            var inPostResponse = await _httpClient.CreateShipmentAsync(inPostRequest);

            return _mapper.MapToCreateParcelResponse(inPostResponse);
        });
    }

    /// <summary>
    /// Gets InPost parcel details for the given parcel identifier.
    /// </summary>
    /// <param name="parcelId">The unique identifier of the parcel. Cannot be null or empty.</param>
    /// <returns>Parcel details for the specified identifier.</returns>
    public Task<GetParcelResponse> GetParcelAsync(string parcelId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(parcelId);

        return ExecuteLoggedAsync(nameof(GetParcelAsync), async () =>
        {
            var inPostResponse = await _httpClient.GetParcelAsync(parcelId);
            return _mapper.MapToGetParcelResponse(inPostResponse);
        });
    }

    /// <summary>
    /// Retrieves shipment label bytes for the given parcel identifier.
    /// </summary>
    /// <param name="parcelId">The unique identifier of the parcel for which to retrieve the label. Cannot be null or empty.</param>
    /// <param name="format">The format of the label to retrieve. Defaults to PDF if not specified.</param>
    /// <returns>Byte array representing the shipment label in the specified format.</returns>
    public Task<byte[]> GetLabelAsync(string parcelId, LabelFormat format = LabelFormat.Pdf)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(parcelId);

        return ExecuteLoggedAsync(nameof(GetLabelAsync), () => _httpClient.GetLabelAsync(parcelId, format));
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
