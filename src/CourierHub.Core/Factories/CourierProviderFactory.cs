using CourierHub.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Core.Factories;

/// <summary>
/// Courier provider factory responsible for managing and providing access to registered courier providers. This factory allows retrieval of specific courier providers by name, as well as access to all registered providers. It serves as a central point for managing courier provider instances within the application.
/// </summary>
public sealed class CourierProviderFactory : ICourierProviderFactory
{
    /// <summary>
    /// A collection of courier provider implementations used to perform courier-related operations.
    /// </summary>
    /// <remarks>This field is intended for internal use to aggregate multiple courier providers. The
    /// order and contents of the collection may affect provider selection logic.</remarks>
    private readonly IEnumerable<ICourierProvider> _providers;

    /// <summary>
    /// Initializes a new instance of the CourierProviderFactory class with the specified collection of courier
    /// providers.
    /// </summary>
    /// <param name="providers">The collection of courier providers to be managed by the factory.</param>
    public CourierProviderFactory(IEnumerable<ICourierProvider> providers)
    {
        _providers = providers;
    }

    /// <summary>
    /// Retrieves the courier provider instance that matches the specified provider name.
    /// </summary>
    /// <param name="providerName">The name of the courier provider to retrieve. The comparison is case-insensitive.</param>
    /// <returns>An instance of <see cref="ICourierProvider"/> that matches the specified provider name.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a courier provider with the specified name is not registered.</exception>
    public ICourierProvider GetProvider(string providerName)
    {
        var provider = _providers.FirstOrDefault(p =>
            p.Name.Equals(providerName, StringComparison.OrdinalIgnoreCase))
            ?? throw new KeyNotFoundException($"Courier provider '{providerName}' is not registered.");
        return provider;
    }

    /// <summary>
    /// Returns a collection of all available courier providers.
    /// </summary>
    /// <returns>An enumerable collection of objects that implement the ICourierProvider interface. The collection may be
    /// empty if no providers are available.</returns>
    public IEnumerable<ICourierProvider> GetAllProviders() => _providers;
}
