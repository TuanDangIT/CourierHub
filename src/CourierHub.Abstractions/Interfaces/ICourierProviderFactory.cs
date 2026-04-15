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
        ICourierProvider GetProvider(string providerName);

        /// <summary>
        /// Returns all registered courier providers. Useful for UI dropdowns.
        /// </summary>
        IEnumerable<ICourierProvider> GetAllProviders();
    }
}
