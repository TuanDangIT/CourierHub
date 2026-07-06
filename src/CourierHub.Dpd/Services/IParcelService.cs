using System.Threading.Tasks;
using CourierHub.Dpd.Client.Models.Requests;
using CourierHub.Dpd.Client.Models.Responses;

namespace CourierHub.Dpd.Services;

/// <summary>
/// DPD parcel service interface defining the contract for parcel-related operations.
/// </summary>
public interface IParcelService
{
    /// <summary>
    /// Creates a DPD shipment.
    /// </summary>
    /// <param name="request">The parcel creation request.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created parcel response.</returns>
    Task<CreateParcelResponse> CreateParcelAsync(CreateParcelRequest request);

    /// <summary>
    /// Retrieves the shipment label bytes.
    /// </summary>
    /// <param name="request">The request containing the parcel identifier and label settings.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the label bytes.</returns>
    Task<byte[]> GetLabelAsync(GetLabelRequest request);
}
