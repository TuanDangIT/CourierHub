using CourierHub.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Core.Factories
{
    internal class CourierProviderFactory : ICourierProviderFactory
    {
        private readonly IEnumerable<ICourierProvider> _providers;

        public CourierProviderFactory(IEnumerable<ICourierProvider> providers)
        {
            _providers = providers;
        }

        public ICourierProvider GetProvider(string providerName)
        {
            var provider = _providers.FirstOrDefault(p =>
                p.Name.Equals(providerName, StringComparison.OrdinalIgnoreCase)) 
                ?? throw new KeyNotFoundException($"Courier provider '{providerName}' is not registered.");
            return provider;
        }

        public IEnumerable<ICourierProvider> GetAllProviders() => _providers;
    }
}
