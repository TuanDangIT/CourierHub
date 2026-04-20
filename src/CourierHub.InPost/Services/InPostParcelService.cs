using CourierHub.Abstractions.Interfaces;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using CourierHub.InPost.Client;
using CourierHub.InPost.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Services
{
    internal class InPostParcelService : IParcelService
    {
        private readonly InPostHttpClient _httpClient;
        private readonly InPostMapper _mapper;

        public InPostParcelService(InPostHttpClient httpClient, InPostMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }
        public Task<CreateParcelResponse> CreateParcelAsync(CreateParcelRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetLabelAsync(string parcelId)
        {
            throw new NotImplementedException();
        }
    }
}
