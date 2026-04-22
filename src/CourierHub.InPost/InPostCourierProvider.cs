using CourierHub.Abstractions.Interfaces;
using CourierHub.Core.Base;
using CourierHub.Core.Configuration;
using CourierHub.InPost.Client;
using CourierHub.InPost.Configuration;
using CourierHub.InPost.Mappers;
using CourierHub.InPost.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost
{
    /// <summary>
    /// InPost courier provider implementation that integrates with the InPost API to manage parcel shipments.
    /// </summary>
    internal class InPostCourierProvider : CourierProviderBase
    {
        /// <summary>
        /// HttpClient instance for making API calls to InPost. 
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// InPost options containing API credentials and configuration settings necessary for authenticating and interacting with the InPost API.
        /// </summary>
        private readonly InPostOptions _inPostOptions;

        /// <summary>
        /// Http resilience options that define retry policies, backoff strategies, and other fault-handling mechanisms for API calls to ensure robustness against transient failures when communicating with the InPost API.
        /// </summary>
        private readonly HttpResilienceOptions _httpResilienceOptions;

        /// <summary>
        /// Name of the provider.
        /// </summary>
        public override string Name => "InPost";

        /// <summary>
        /// Initializes a new instance of the InPostCourierProvider class with the specified dependencies.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance for making API calls to InPost.</param>
        /// <param name="inPostOptions">The InPost options containing API credentials and configuration settings.</param>
        /// <param name="httpResilienceOptions">The Http resilience options for handling transient failures.</param>
        /// <param name="logger">The logger instance for logging.</param>
        public InPostCourierProvider(
            HttpClient httpClient, 
            InPostOptions inPostOptions, 
            HttpResilienceOptions httpResilienceOptions, 
            ILogger? logger = default) : base(logger)
        {
            _httpClient = httpClient;
            _inPostOptions = inPostOptions;
            _httpResilienceOptions = httpResilienceOptions;
        }

        /// <summary>
        /// ParcelService implementation that provides methods for managing parcels.
        /// </summary>
        public override IParcelService? ParcelService 
            => new InPostParcelService(
                new InPostHttpClient(_httpClient, _inPostOptions, _httpResilienceOptions, _logger), 
                new InPostMapper(), 
                _logger);
    }
}
