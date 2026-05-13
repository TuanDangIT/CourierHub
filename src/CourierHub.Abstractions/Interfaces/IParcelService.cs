using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Interfaces
{
    /// <summary>
    /// Defines the contract for parcel-related operations within the application.
    /// </summary>
    public interface IParcelService
    {
        /// <summary>
        /// Creates a new parcel based on the provided request.
        /// </summary>
        /// <param name="request">The request containing parcel details.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="CreateParcelResponse"/> result.</returns>
        Task<CreateParcelResponse> CreateParcelAsync(CreateParcelRequest request);


        /// <summary>
        /// Retrieves the shipping label for the specified parcel.
        /// </summary>
        /// <param name="request">The request containing the details for retrieving the label.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a byte array with the label data.</returns>
        Task<byte[]> GetLabelAsync(GetLabelRequest request);
    }
}
