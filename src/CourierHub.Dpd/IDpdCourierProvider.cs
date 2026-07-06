using CourierHub.Dpd.Services;

namespace CourierHub.Dpd;

/// <summary>
/// DPD courier provider interface.
/// </summary>
public interface IDpdCourierProvider
{
    /// <summary>
    /// Parcel service for managing parcels.
    /// </summary>
    IParcelService Parcels { get; }
}
