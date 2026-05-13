namespace CourierHub.Dpd.Client.Models.Responses;

/// <summary>
/// Represents the response from DPD API when creating a shipment.
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
    /// Begin time of the session in DPD system. This timestamp indicates when the shipment creation request was initiated and processing started.
    /// </summary>
    public required DateTime BeginTime { get; init; }

    /// <summary>
    /// End time of the session in DPD system. This timestamp indicates when the shipment creation request was completed and processing ended.
    /// </summary>
    public required DateTime EndTime { get; init; }

    /// <summary>
    /// The packages created in this shipment, each containing parcels with waybills.
    /// </summary>
    public required IEnumerable<DpdCreateParcelResponsePackage> Packages { get; init; }

    /// <summary>
    /// The trace ID for tracking this request in DPD system.
    /// </summary>
    public required string TraceId { get; init; }
}

/// <summary>
/// Represents a package in a DPD shipment response.
/// </summary>
public sealed class DpdCreateParcelResponsePackage
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
    public required IEnumerable<DpdCreateParcelResponseParcel> Parcels { get; init; }
}

/// <summary>
/// Represents a parcel in a DPD package response.
/// Contains the waybill (tracking number) for this parcel.
/// </summary>
public sealed class DpdCreateParcelResponseParcel
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