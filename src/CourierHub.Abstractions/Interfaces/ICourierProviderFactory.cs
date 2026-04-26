using CourierHub.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Interfaces
{
    /// <summary>
    /// Interface used for retrieving courier providers based on known enum values or extensible string identifiers.
    /// </summary>
    public interface ICourierProviderFactory
    {
        /// <summary>
        /// Returns a built-in courier provider by enum value.
        /// </summary>
        /// <param name="courierProvider">The built-in courier provider enum value.</param>
        /// <returns>The courier provider associated with the specified enum value.</returns>
        ICourierProvider GetProvider(CourierProvider courierProvider);

        /// <summary>
        /// Returns a courier provider by string identifier.
        /// </summary>
        /// <param name="providerName">Stable provider identifier (for example: "InPost", "MyCustomProvider").</param>
        /// <returns>The courier provider associated with the specified identifier.</returns>
        ICourierProvider GetProvider(string providerName);

        /// <summary>
        /// Returns all registered courier providers.
        /// </summary>
        /// <returns>An enumerable collection of all registered courier providers.</returns>
        IEnumerable<ICourierProvider> GetAllProviders();
    }
}
