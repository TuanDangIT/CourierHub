using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Interfaces;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Services
{
    internal class DpdParcelService : IParcelService
    {
        public Task<CreateParcelResponse> CreateParcelAsync(CreateParcelRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetLabelAsync(string parcelId, LabelFormat labelFormat = LabelFormat.Pdf)
        {
            throw new NotImplementedException();
        }
    }
}
