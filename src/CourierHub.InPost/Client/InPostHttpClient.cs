using CourierHub.Core.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client
{
    internal class InPostHttpClient : HttpClientBase
    {
        public InPostHttpClient(HttpClient httpClient, ILogger? logger = null) : base(httpClient, logger)
        {
        }
    }
}
