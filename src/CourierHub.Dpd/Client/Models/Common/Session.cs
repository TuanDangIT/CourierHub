using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Client.Models.Common;

/// <summary>
/// DPD specific session information. 
/// </summary>
public sealed class Session
{
    /// <summary>
    /// Type of the session. Possible values are:
    /// - DOMESTIC - Session for domestic packages
    /// - INTERNATIONAL - Session for international packages
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Session ID assigned by DPD for shipment creation.
    /// </summary>
    public string? SessionId { get; set; }

    /// <summary>
    /// Packages associated with this session.
    /// </summary>
    public IEnumerable<SessionPackage>? Packages { get; set; }
}

/// <summary>
/// DPD session package information. This class represents a single package within a DPD session, including its reference and the parcels it contains.
/// </summary>
public sealed class SessionPackage
{
    /// <summary>
    /// Reference for the package, if applicable.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Parcels included in this package.
    /// </summary>
    public IEnumerable<SessionParcel> Parcels { get; set; } = [];
}

/// <summary>
/// DPD session parcel information. This class represents a single parcel within a DPD session, including its reference and waybill details.
/// </summary>
public sealed class SessionParcel
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
