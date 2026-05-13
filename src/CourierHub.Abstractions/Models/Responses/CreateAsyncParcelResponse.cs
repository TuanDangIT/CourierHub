using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Responses;

/// <summary>
/// Represents the response from a successful asynchronous parcel creation request. This response is returned when a parcel creation request is accepted for processing but the final status of the parcel (e.g., label generation, shipment confirmation) is not yet available at the time of the response. The caller can use the provided ParcelId to poll for the final status of the parcel creation process using a separate API endpoint designed for status retrieval.
/// </summary>
public record class CreateAsyncParcelResponse
{
    /// <summary>
    /// The unique identifier for the parcel assigned by the courier provider.
    /// </summary>
    /// <remarks>
    /// This ID is used for subsequent API calls to retrieve labels, cancel shipments,
    /// or update parcel information depending on courier provider. Some actions might be not allowed.
    /// </remarks>
    public required string ParcelId { get; init; }

    /// <summary>
    /// The status of the parcel creation request at the time of the response. This status indicates that the request has been accepted for processing, but the final outcome (e.g., successful creation, failure, pending) is not yet determined. The specific status values and their meanings may vary depending on the courier provider's API contract. The caller can use this status to determine whether to start polling for updates or handle the response accordingly.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// The tracking number for the shipment. This value can be null.
    /// </summary>
    /// <remarks>
    /// This number can be used for tracking the parcel's delivery status and is
    /// typically displayed on shipping labels and communicated to customers.
    /// </remarks>
    public IEnumerable<string?> TrackingNumbers { get; init; } = [];
}

/// <summary>
/// Represents the response from a successful asynchronous parcel creation request. This response is returned when a parcel creation request is accepted for processing but the final status of the parcel (e.g., label generation, shipment confirmation) is not yet available at the time of the response. The caller can use the provided ParcelId to poll for the final status of the parcel creation process using a separate API endpoint designed for status retrieval.
/// </summary>
/// <typeparam name="TExtension">The type of the courier-specific response extension, which must implement the ICourierResponseExtension interface. This allows for including additional courier-specific information in the response while maintaining a standardized structure for common fields.</typeparam>
public record class CreateAsyncParcelResponse<TExtension> : CreateAsyncParcelResponse where TExtension : ICourierResponseExtension
{
    /// <summary>
    /// The status of the parcel creation request at the time of the response, represented as an enumeration. This status indicates that the request has been accepted for processing, but the final outcome (e.g., successful creation, failure, pending) is not yet determined. The specific status values and their meanings are defined in the ParcelCreationStatus enum and may vary depending on the courier provider's API contract. The caller can use this status to determine whether to start polling for updates or handle the response accordingly.
    /// </summary>
    public TExtension? Extension { get; init; }
}
