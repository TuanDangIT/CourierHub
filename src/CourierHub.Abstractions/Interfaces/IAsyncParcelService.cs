using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Interfaces
{
    /// <summary>
    /// Defines the contract for asynchronous parcel operations, allowing for non-blocking interactions with parcel-related functionalities such as creation, tracking, and management of shipments. This interface for now is only intended for InPost api integration.
    /// </summary>
    public interface IAsyncParcelService
    {
        /// <summary>
        /// Creates a new parcel asynchronously based on the provided request. Asynchronously here means that the label will not be ready immediately and will need to check the status, in order to successfully retrieve the label.
        /// </summary>
        /// <typeparam name="TExtension">The type of courier-specific response extension.</typeparam>
        /// <param name="request">The request containing parcel details.</param>
        /// <returns>A task representing the asynchronous operation, with a strongly typed asynchronous parcel creation response.</returns>
        Task<CreateAsyncParcelResponse<TExtension>> CreateParcelAsync<TExtension>(CreateParcelRequest request)
            where TExtension : ICourierResponseExtension;

        /// <summary>
        /// Gets the status of a parcel creation asynchronously. This method is used to check the status of a parcel creation after initiating it with CreateParcelAsync, as the label may not be ready immediately and may require some time to be generated. The response will indicate whether the parcel creation was successful and if the label is ready for retrieval.
        /// </summary>
        /// <param name="parcelId">The unique identifier of the parcel. Definiton depends on courier provider. Cannot be null or empty.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="GetParcelCreationStatusResponse"/> result.</returns>
        Task<GetParcelCreationStatusResponse> GetParcelCreationStatusAsync(string parcelId);

        /// <summary>
        /// Retrieves the shipping label for the specified parcel.
        /// </summary>
        /// <param name="request">The request containing the details for retrieving the label.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a byte array with the label data.</returns>
        Task<byte[]> GetLabelAsync(GetLabelRequest request);
    }
}
