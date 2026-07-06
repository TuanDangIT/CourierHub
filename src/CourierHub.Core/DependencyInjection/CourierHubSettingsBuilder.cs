using CourierHub.Core.Http;
using CourierHub.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;

namespace CourierHub.Core.DependencyInjection;

/// <summary>
/// Fluent builder for registering courier providers.
/// </summary>
public sealed class CourierHubSettingsBuilder
{
    /// <summary>
    /// The DI service collection.
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// The shared HTTP options used by all courier providers.
    /// </summary>
    public HttpOptions Http { get; } = new();

    /// <summary>
    /// The shared logging options used by all courier providers.
    /// </summary>
    public LoggingOptions Logging { get; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CourierHubSettingsBuilder"/> class.
    /// </summary>
    /// <param name="services">The service collection used for registrations.</param>
    public CourierHubSettingsBuilder(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    /// <summary>
    /// Configures the shared HTTP options used by all courier providers.
    /// </summary>
    /// <param name="configure">Callback used to configure the HTTP options.</param>
    /// <returns>The same builder instance for chaining.</returns>
    public CourierHubSettingsBuilder ConfigureHttp(Action<HttpOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        configure(Http);
        return this;
    }

    /// <summary>
    /// Configures the shared logging options used by all courier providers.
    /// </summary>
    /// <param name="configure">Callback used to configure the logging options.</param>
    /// <returns>The same builder instance for chaining.</returns>
    public CourierHubSettingsBuilder ConfigureLogging(Action<LoggerOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        var options = new LoggerOptions();
        configure(options);

        Services.AddLogging(builder =>
        {
            builder.SetMinimumLevel(options.MinimumLevel);
            builder.AddFilter("CourierHub", options.MinimumLevel); 

            foreach (var (category, level) in options.Filters)
                builder.AddFilter(category, level);

            if (options.EnableConsole)
                builder.AddConsole();
        });

        return this;
    }
}
