using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Interfaces;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using CourierHub.Core.Logging;
using CourierHub.Dpd.Client;
using CourierHub.Dpd.Mappers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CourierHub.Dpd.Services;

internal sealed class DpdParcelService : IParcelService
{
    private readonly DpdHttpClient _httpClient;
    private readonly DpdCreateParcelMapper _mapper;
    private readonly ILogger? _logger;

    public DpdParcelService(DpdHttpClient httpClient, DpdCreateParcelMapper mapper, ILogger? logger = default)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _logger = logger;
    }

    public Task<CreateParcelResponse> CreateParcelAsync(CreateParcelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return ExecuteLoggedAsync(nameof(CreateParcelAsync), async () =>
        {
            var dpdRequest = _mapper.MapToCreateParcelRequest(request);
            var dpdResponse = await _httpClient.CreateShipmentAsync(dpdRequest);

            return _mapper.MapToCreateParcelResponse(dpdResponse);
        });
    }

    public Task<byte[]> GetLabelAsync(GetLabelRequest request)
    {
        throw new NotImplementedException();
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
