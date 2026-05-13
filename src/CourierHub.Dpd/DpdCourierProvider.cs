using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Interfaces;
using CourierHub.Core.Base;
using CourierHub.Core.Configuration;
using CourierHub.Dpd.Client;
using CourierHub.Dpd.Configurations;
using CourierHub.Dpd.Mappers;
using CourierHub.Dpd.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd
{
    internal class DpdCourierProvider : CourierProviderBase
    {
        /// <summary>
        /// ParcelService implementation that provides methods for managing parcels.
        /// </summary>
        private readonly IParcelService _parcelService;

        /// <summary>
        /// Stable provider identifier.
        /// </summary>
        public override string Name => "Dpd";

        /// <summary>
        /// Built-in provider enum value.
        /// </summary>
        public override CourierProvider? Provider => CourierProvider.Dpd;

        /// <summary>
        /// Initializes a new instance of the DpdCourierProvider class with the specified dependencies.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance for making API calls to Dpd.</param>
        /// <param name="dpdOptions">The Dpd options containing API credentials and configuration settings.</param>
        /// <param name="httpResilienceOptions">The Http resilience options for handling transient failures.</param>
        /// <param name="logger">The logger instance for logging.</param>
        public DpdCourierProvider(
            HttpClient httpClient,
            DpdOptions dpdOptions,
            HttpResilienceOptions httpResilienceOptions,
            ILogger? logger = default) : base(logger)
        {
            ArgumentNullException.ThrowIfNull(httpClient);
            ArgumentNullException.ThrowIfNull(dpdOptions);
            ArgumentNullException.ThrowIfNull(httpResilienceOptions);

            var dpdHttpClient = new DpdHttpClient(httpClient, dpdOptions, httpResilienceOptions, logger);
            _parcelService = new DpdParcelService(dpdHttpClient, new DpdCreateParcelMapper(), logger);
        }

        /// <summary>
        /// ParcelService implementation that provides methods for managing parcels.
        /// </summary>
        public override IParcelService? ParcelService => _parcelService;
    }
}
