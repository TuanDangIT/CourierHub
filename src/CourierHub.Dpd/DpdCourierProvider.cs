using CourierHub.Core.Base;
using CourierHub.Dpd.Client;
using CourierHub.Dpd.Configurations;
using CourierHub.Dpd.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd
{
    /// <summary>
    /// DPD courier provider implementation that integrates with the DPD API.
    /// </summary>
    public sealed class DpdCourierProvider : CourierProviderBase, IDpdCourierProvider
    {
        /// <summary>
        /// Parcel service for managing shipments and related operations with the DPD API.
        /// </summary>
        public IParcelService Parcels { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DpdCourierProvider"/> class with the specified dependencies.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance for making API calls to DPD.</param>
        /// <param name="dpdOptions">The DPD options containing API credentials and configuration settings.</param>
        /// <param name="loggerFactory">The logger factory instance for creating loggers.</param>
        public DpdCourierProvider(
            HttpClient httpClient,
            DpdOptions dpdOptions,
            ILoggerFactory? loggerFactory = default) : base(loggerFactory)
        {
            ArgumentNullException.ThrowIfNull(httpClient);
            ArgumentNullException.ThrowIfNull(dpdOptions);

            var dpdHttpClient = new DpdHttpClient(httpClient, dpdOptions, loggerFactory?.CreateLogger<DpdHttpClient>());
            Parcels = new ParcelService(dpdHttpClient, loggerFactory?.CreateLogger<ParcelService>());
        }
    }
}
