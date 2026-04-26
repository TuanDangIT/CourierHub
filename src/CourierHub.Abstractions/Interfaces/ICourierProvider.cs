using CourierHub.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Interfaces
{
    /// <summary>
    /// Defines the contract for a courier provider, exposing services for lockers, pickups, and returns, as well as provider identification.
    /// </summary>
    /// <remarks>
    /// Implementations may expose a known built-in provider via <see cref="Provider"/>,
    /// and should always expose a stable string identifier via <see cref="Name"/> for extensibility.
    /// </remarks>
    public interface ICourierProvider
    {
        /// <summary>
        /// Stable provider identifier (for example: "InPost", "Dpd", "MyCustomProvider").
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Built-in provider enum value when applicable; otherwise <c>null</c> for custom/third-party providers.
        /// </summary>
        CourierProvider? Provider { get; }

        /// <summary>
        /// The parcel service used to manage parcel operations.
        /// </summary>
        IParcelService? ParcelService { get; }

        /// <summary>
        /// The locker service used to manage lockers.
        /// </summary>
        ILockerService? Lockers { get; }

        /// <summary>
        /// The pickup service used to manage pickups.
        /// </summary>
        IPickupService? Pickups { get; }

        /// <summary>
        /// The service used to process parcel returns.
        /// </summary>
        IReturnService? Returns { get; }

        /// <summary>
        /// The financial service used for estimating parcel costs and managing COD.
        /// </summary>
        IFinancialService? Finance { get; }
    }
}
