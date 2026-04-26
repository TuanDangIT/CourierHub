using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Core.Factories;

/// <summary>
/// Courier provider factory responsible for managing and providing access to registered courier providers.
/// </summary>
public sealed class CourierProviderFactory : ICourierProviderFactory
{
    /// <summary>
    /// A collection of courier provider implementations used to perform courier-related operations.
    /// </summary>
    private readonly IEnumerable<ICourierProvider> _providers;

    /// <summary>
    /// Initializes a new instance of the CourierProviderFactory class with the specified collection of courier providers.
    /// </summary>
    /// <param name="providers">The collection of courier providers to be managed by the factory.</param>
    public CourierProviderFactory(IEnumerable<ICourierProvider> providers)
    {
        _providers = providers;
    }

    /// <summary>
    /// Retrieves a built-in courier provider by enum value.
    /// </summary>
    /// <param name="courierProvider">The built-in courier provider enum value.</param>
    /// <returns>The matching courier provider.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a matching provider is not registered.</exception>
    public ICourierProvider GetProvider(CourierProvider courierProvider)
    {
        var provider = _providers.FirstOrDefault(p => p.Provider == courierProvider)
            ?? throw new KeyNotFoundException($"Courier provider '{courierProvider}' is not registered.");

        return provider;
    }

    /// <summary>
    /// Retrieves a courier provider by string identifier.
    /// </summary>
    /// <param name="providerName">The stable provider identifier.</param>
    /// <returns>The matching courier provider.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if a matching provider is not registered.</exception>
    public ICourierProvider GetProvider(string providerName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(providerName);

        var provider = _providers.FirstOrDefault(p =>
            p.Name.Equals(providerName, StringComparison.OrdinalIgnoreCase))
            ?? throw new KeyNotFoundException($"Courier provider '{providerName}' is not registered.");

        return provider;
    }

    /// <summary>
    /// Returns a collection of all available courier providers.
    /// </summary>
    /// <returns>An enumerable collection of registered courier providers.</returns>
    public IEnumerable<ICourierProvider> GetAllProviders() => _providers;
}
