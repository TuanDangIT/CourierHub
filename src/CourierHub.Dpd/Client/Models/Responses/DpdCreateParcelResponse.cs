namespace CourierHub.Dpd.Client.Models.Responses;

/// <summary>
/// Represents the response from DPD API when creating a shipment.
/// Contains session information and packages with their parcels and waybills.
/// </summary>
public sealed class DpdCreateParcelResponse
{
    /// <summary>
    /// The status of the shipment creation request.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// The session ID assigned by DPD for this shipment creation.
    /// </summary>
    public required int SessionId { get; init; }

    /// <summary>
    /// The packages created in this shipment, each containing parcels with waybills.
    /// </summary>
    public required List<DpdCreateParcelPackage> Packages { get; init; }

    /// <summary>
    /// The trace ID for tracking this request in DPD system.
    /// </summary>
    public required string TraceId { get; init; }
}

/// <summary>
/// Represents a package in a DPD shipment response.
/// </summary>
public sealed class DpdCreateParcelPackage
{
    /// <summary>
    /// The status of the package.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// Optional reference for the package.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// The parcels in this package, each with a waybill.
    /// </summary>
    public required List<DpdCreateParcelParcel> Parcels { get; init; }
}

/// <summary>
/// Represents a parcel in a DPD package response.
/// Contains the waybill (tracking number) for this parcel.
/// </summary>
public sealed class DpdCreateParcelParcel
{
    /// <summary>
    /// The status of the parcel.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// Optional reference for the parcel.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// The waybill number (tracking number) for this parcel.
    /// </summary>
    public required string Waybill { get; init; }
}