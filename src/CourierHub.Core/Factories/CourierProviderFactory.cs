using CourierHub.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Core.Factories
{
    internal class CourierProviderFactory : ICourierProviderFactory
    {
        public IEnumerable<ICourierProvider> GetAllProviders()
        {
            throw new NotImplementedException();
        }

        public ICourierProvider GetProvider(string providerName)
        {
            throw new NotImplementedException();
        }
    }
}
