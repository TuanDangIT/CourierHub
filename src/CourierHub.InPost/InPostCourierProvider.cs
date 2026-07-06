using CourierHub.Core.Base;
using CourierHub.InPost.Client;
using CourierHub.InPost.Configurations;
using CourierHub.InPost.Services;
using Microsoft.Extensions.Logging;

namespace CourierHub.InPost;

/// <summary>
/// InPost courier provider implementation that integrates with the InPost API.
/// </summary>
public sealed class InPostCourierProvider : CourierProviderBase, IInpostCourierProvider
{
    /// <summary>
    /// Parcel service for managing shipments and related operations with the InPost API.
    /// </summary>
    public IParcelService Parcels { get; }

    /// <summary>
    /// Initializes a new instance of the InPostCourierProvider class with the specified dependencies.
    /// </summary>
    /// <param name="httpClient">The HttpClient instance for making API calls to InPost.</param>
    /// <param name="inPostOptions">The InPost options containing API credentials and configuration settings.</param>
    /// <param name="loggerFactory">The logger factory for creating loggers.</param>
    public InPostCourierProvider(
        HttpClient httpClient,
        InPostOptions inPostOptions,
        ILoggerFactory? loggerFactory = default) : base(loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(inPostOptions);

        var inPostHttpClient = new InPostHttpClient(httpClient, inPostOptions, loggerFactory?.CreateLogger<InPostHttpClient>());
        Parcels = new ParcelService(inPostHttpClient, loggerFactory?.CreateLogger<ParcelService>());
    }
}
