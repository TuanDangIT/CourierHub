using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Client.Models.Common;

/// <summary>
/// Dpd specific session information. 
/// </summary>
internal sealed class DpdSession
{
    /// <summary>
    /// Type of the session. Possible values are:
    /// - DOMESTIC - Session for domestic packages
    /// - INTERNATIONAL - Session for international packages
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Session ID assigned by Dpd for shipment creation.
    /// </summary>
    public string? SessionId { get; set; }

    /// <summary>
    /// Packages associated with this session.
    /// </summary>
    public IEnumerable<DpdSessionPackage> Packages { get; set; } = [];
}

/// <summary>
/// Dpd session package information. This class represents a single package within a Dpd session, including its reference and the parcels it contains.
/// </summary>
internal sealed class DpdSessionPackage
{
    /// <summary>
    /// Reference for the package, if applicable.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Parcels included in this package.
    /// </summary>
    public IEnumerable<DpdSessionParcel> Parcels { get; set; } = [];
}

/// <summary>
/// Dpd session parcel information. This class represents a single parcel within a Dpd session, including its reference and waybill details.
/// </summary>
internal sealed class DpdSessionParcel
{
    /// <summary>
    /// Reference for the parcel, if applicable.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Waybill for the parcel, if applicable.
    /// </summary>
    public string? Waybill { get; set; }
}