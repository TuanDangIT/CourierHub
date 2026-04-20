using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Interfaces
{
    /// <summary>
    /// Defines the contract for a courier provider, exposing services for lockers, pickups, and returns, as well as the
    /// provider's name.
    /// </summary>
    /// <remarks>Implementations of this interface represent integrations with specific courier services. Not
    /// all providers may support all services; properties may return null if a particular service is
    /// unavailable.</remarks>
    public interface ICourierProvider
    {
        /// <summary>
        /// Gets the name associated with the current instance.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the parcel service used to manage parcel operations.
        /// </summary>
        IParcelService? ParcelService { get; }
        /// <summary>
        /// Gets the locker service used to manage lockers.
        /// </summary>
        ILockerService? Lockers { get; }
        /// <summary>
        /// Gets the pickup service used to manage pickups.
        /// </summary>
        IPickupService? Pickups { get; }
        /// <summary>
        /// Gets the service used to process parcel returns.
        /// </summary>
        IReturnService? Returns { get; }
        /// <summary>
        /// Gets the financial service used for estimating parcel costs and managing COD.
        /// </summary>
        IFinancialService? Finance { get; }
    }
}
