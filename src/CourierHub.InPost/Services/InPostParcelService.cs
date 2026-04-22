using CourierHub.Abstractions.Interfaces;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using CourierHub.InPost.Client;
using CourierHub.InPost.Mappers;
using Microsoft.Extensions.Logging;

namespace CourierHub.InPost.Services
{
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
        public async Task<CreateParcelResponse> CreateParcelAsync(CreateParcelRequest request)
        {
            var inPostRequest = _mapper.MapToCreateParcelRequest(request);
            var inPostResponse = await _httpClient.CreateShipmentAsync(inPostRequest);

            return _mapper.MapToCreateParcelResponse(inPostResponse);
        }

        /// <summary>
        /// Retrieves shipment label bytes for the given parcel identifier.
        /// </summary>
        public async Task<byte[]> GetLabelAsync(string parcelId)
        {
            if (string.IsNullOrWhiteSpace(parcelId))
            {
                throw new ArgumentException("Parcel ID cannot be null or empty.", nameof(parcelId));
            }

            return await _httpClient.GetLabelAsync(parcelId);
        }
    }
}
