using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Interfaces;
using CourierHub.Core.Base;
using CourierHub.Core.Configuration;
using CourierHub.InPost.Client;
using CourierHub.InPost.Configurations;
using CourierHub.InPost.Mappers;
using CourierHub.InPost.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost;

/// <summary>
/// InPost courier provider implementation that integrates with the InPost API.
/// </summary>
public sealed class InPostCourierProvider : CourierProviderBase
{
    /// <summary>
    /// Stable provider identifier.
    /// </summary>
    public override string Name => "InPost";

    /// <summary>
    /// Built-in provider enum value.
    /// </summary>
    public override CourierProvider? Provider => CourierProvider.InPost;

    /// <summary>
    /// AsyncParcelService implementation that provides methods for managing parcels asynchronously.
    /// </summary>
    public override IAsyncParcelService? AsyncParcelService => _asyncParcelService;

    /// <summary>
    /// AsyncParcelService implementation that provides methods for managing parcels asynchronously.
    /// </summary>
    private readonly IAsyncParcelService _asyncParcelService;

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
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(inPostOptions);
        ArgumentNullException.ThrowIfNull(httpResilienceOptions);

        _asyncParcelService = new InPostAsyncParcelService(
            new InPostHttpClient(httpClient, inPostOptions, httpResilienceOptions, _logger),
            new InPostMapper(),
            _logger);
    }
}
