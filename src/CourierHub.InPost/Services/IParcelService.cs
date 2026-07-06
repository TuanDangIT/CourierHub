using System.Threading.Tasks;
using CourierHub.InPost.Client.Models.Requests;
using CourierHub.InPost.Client.Models.Responses;

namespace CourierHub.InPost.Services;

/// <summary>
/// Parcel service interface defining the contract for parcel-related operations.
/// </summary>
public interface IParcelService
{
    /// <summary>
    /// Create parcel asynchronously based on the provided request data and returns the response containing details of the created parcel.
    /// </summary>
    /// <remarks>This operation is fully asynchronous on InPost side, meaning that a label will not be created immediately and that user will have to constantly check the status of the operation.</remarks>
    /// <param name="request">The request data for creating the parcel.</param>
    /// <returns>A task that represents the pure asynchronous operation. The task result contains the response with details of the created parcel.</returns>
    Task<CreateParcelResponse> CreateParcelAsync(CreateParcelRequest request);

    /// <summary>
    /// Creates multiple parcels in a single batch operation.
    /// </summary>
    /// <param name="request">The batch creation request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created batch response.</returns>
    Task<CreateBatchParcelsResponse> CreateBatchParcelsAsync(CreateBatchParcelsRequest request);

    /// <summary>
    /// Pays for a shipment by selecting one of its offers.
    /// </summary>
    /// <param name="request">The payment request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated shipment response.</returns>
    Task<PayForParcelResponse> PayForParcelAsync(PayForParcelRequest request);

    /// <summary>
    /// Retrieves the shipment label bytes for a shipment.
    /// </summary>
    /// <param name="request">The request data for retrieving the label.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the label bytes.</returns>
    Task<byte[]> GetLabelAsync(GetLabelRequest request);

    /// <summary>
    /// Retrieves shipments matching the provided filters.
    /// </summary>
    /// <param name="request">The shipments list request.</param>
    /// <returns>The paged shipments response.</returns>
    Task<GetParcelsResponse> GetParcelsAsync(GetParcelsRequest request);

    /// <summary>
    /// Retrieves a batch by identifier.
    /// </summary>
    /// <param name="request">The batch lookup request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the batch response.</returns>
    Task<GetBatchParcelsResponse> GetBatchParcelsAsync(GetBatchParcelsRequest request);
}
