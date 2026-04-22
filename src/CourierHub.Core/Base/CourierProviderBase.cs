using CourierHub.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Core.Base
{
    /// <summary>
    /// Provides a base implementation for courier service providers, exposing common service interfaces and logging
    /// functionality.
    /// </summary>
    /// <remarks>Inherit from this class to implement specific courier provider integrations. Derived classes
    /// should override the relevant service properties to expose supported features. The base class provides a logger
    /// for use in subclasses.</remarks>
    public abstract class CourierProviderBase : ICourierProvider
    {
        /// <summary>
        /// Provides access to the logger instance used for recording diagnostic and operational messages within the
        /// containing class.
        /// </summary>
        /// <remarks>Intended for use by derived classes to facilitate consistent logging. The logger
        /// instance is typically configured by the containing class or its dependencies.</remarks>
        protected readonly ILogger? _logger;

        /// <summary>
        /// Initializes a new instance of the CourierProviderBase class with the specified logger.
        /// </summary>
        /// <param name="logger">The logger instance used to record diagnostic and operational information. Cannot be null.</param>
        protected CourierProviderBase(ILogger? logger = default)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the name associated with the current instance.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the parcel service used to manage parcel operations.
        /// </summary>
        public virtual IParcelService? ParcelService => null;

        /// <summary>
        /// Gets the locker service used to manage lockers.
        /// </summary>
        public virtual ILockerService? Lockers => null;

        /// <summary>
        /// Gets the pickup service used to manage pickups.
        /// </summary>
        public virtual IPickupService? Pickups => null;

        /// <summary>
        /// Gets the service used to process parcel returns.
        /// </summary>
        public virtual IReturnService? Returns => null;

        /// <summary>
        /// Gets the financial service used for estimating parcel costs and managing COD.
        /// </summary>
        public virtual IFinancialService? Finance => null;

        /// <summary>
        /// Logs a request with the specified action and identifier.
        /// </summary>
        /// <param name="action">The action being executed.</param>
        /// <param name="identifier">The identifier associated with the request.</param>
        protected void LogRequest(string action, string identifier) 
            => _logger?.LogInformation("[{CourierName}] Executing {Action} for {Id}", Name, action, identifier);
    }
}
