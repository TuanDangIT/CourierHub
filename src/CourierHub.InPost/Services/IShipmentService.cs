using CourierHub.Core.Result;
using CourierHub.InPost.Client.Models.Requests;
using CourierHub.InPost.Client.Models.Responses;
using System.Threading.Tasks;

namespace CourierHub.InPost.Services;

/// <summary>
/// Parcel service interface defining the contract for parcel-related operations.
/// </summary>
public interface IShipmentService
{
    /// <summary>
    /// Create parcel asynchronously based on the provided request data and returns the response containing details of the created parcel.
    /// </summary>
    /// <remarks>This operation is fully asynchronous on InPost side, meaning that a label will not be created immediately and that user will have to constantly check the status of the operation.</remarks>
    /// <param name="request">The request data for creating the shipment.</param>
    /// <returns>A task that represents the pure asynchronous operation. The task result contains the response with details of the created shipment.</returns>
    Task<Result<CreateShipmentResponse>> CreateShipmentAsync(CreateShipmentRequest request);

    /// <summary>
    /// Creates multiple shipments in a single batch operation.
    /// </summary>
    /// <param name="request">The batch creation request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created batch response.</returns>
    Task<Result<CreateShipmentBatchResponse>> CreateShipmentBatchAsync(CreateShipmentBatchRequest request);
    /// <summary>
    /// Pays for a shipment by selecting one of its offers.
    /// </summary>
    /// <param name="shipmentId">The identifier of the shipment to pay for.</param>
    /// <param name="request">The payment request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated shipment response.</returns>
    Task<Result<PayForShipmentResponse>> PayForShipmentAsync(string shipmentId, PayForShipmentRequest request);

    /// <summary>
    /// Retrieves the shipment label bytes for a shipment.
    /// </summary>
    /// <param name="request">The request data for retrieving the label.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the label bytes.</returns>
    Task<Result<byte[]>> GetLabelAsync(GetLabelRequest request);

    /// <summary>
    /// Retrieves shipments matching the provided filters.
    /// </summary>
    /// <param name="request">The shipments list request.</param>
    /// <returns>The paged shipments response.</returns>
    Task<Result<GetShipmentsResponse>> GetShipmentsAsync(GetShipmentsRequest request);

    /// <summary>
    /// Retrieves a batch by identifier.
    /// </summary>
    /// <param name="request">The batch lookup request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the batch response.</returns>
    Task<Result<GetShipmentBatchResponse>> GetShipmentBatchAsync(GetShipmentBatchRequest request);
}
