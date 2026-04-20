using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Interfaces
{
    /// <summary>
    /// Interface used for retrieving courier providers based on their unique names.
    /// </summary>
    public interface ICourierProviderFactory
    {
        /// <summary>
        /// Returns the specific courier provider by its unique name.
        /// </summary> 
        /// <param name="providerName">The unique name of the courier provider.</param>
        /// <returns>The courier provider associated with the specified name.</returns>
        ICourierProvider GetProvider(string providerName);

        /// <summary>
        /// Returns all registered courier providers. Useful for UI dropdowns.
        /// </summary>
        /// <returns>An enumerable collection of all registered courier providers.</returns>
        IEnumerable<ICourierProvider> GetAllProviders();
    }
}
