using CourierHub.Core.DependencyInjection;
using CourierHub.Dpd.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;

namespace CourierHub.Dpd.DependencyInjection;

/// <summary>
/// Fluent registration extensions for Dpd provider.
/// </summary>
public static class DpdCourierSettingsBuilderExtensions
{
    /// <summary>
    /// Registers Dpd provider and configuration.
    /// </summary>
    /// <param name="builder">Courier settings builder.</param>
    /// <param name="setupAction">Action used to configure Dpd options.</param>
    /// <returns>The same builder instance for fluent chaining.</returns>
    public static CourierHubSettingsBuilder AddDpd(this CourierHubSettingsBuilder builder, Action<DpdOptions> setupAction)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(setupAction);

        var dpdOptions = new DpdOptions
        {
            BaseUrl = string.Empty,
            Login = string.Empty,
            Password = string.Empty,
            MasterFID = string.Empty
        };
        setupAction(dpdOptions);

        ArgumentException.ThrowIfNullOrWhiteSpace(dpdOptions.BaseUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(dpdOptions.Login);
        ArgumentException.ThrowIfNullOrWhiteSpace(dpdOptions.Password);
        ArgumentException.ThrowIfNullOrWhiteSpace(dpdOptions.MasterFID);

        builder.Services.AddSingleton(dpdOptions);

        var sharedHttpOptions = builder.Http;
        builder.Services
            .AddHttpClient<IDpdCourierProvider, DpdCourierProvider>((sp, httpClient) =>
            {
                httpClient.Timeout = sharedHttpOptions.Timeout;
                httpClient.BaseAddress = new Uri(sp.GetRequiredService<DpdOptions>().BaseUrl, UriKind.Absolute);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new SocketsHttpHandler();
                if (sharedHttpOptions.PooledConnectionLifetime is { } pooledConnectionLifetime)
                {
                    handler.PooledConnectionLifetime = pooledConnectionLifetime;
                }

                return handler;
            })
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = sharedHttpOptions.Retry.MaxRetryAttempts;
                options.Retry.Delay = sharedHttpOptions.Retry.Delay;
                options.Retry.MaxDelay = sharedHttpOptions.Retry.MaxDelay;
                options.Retry.UseJitter = sharedHttpOptions.Retry.UseJitter;
            });

        return builder;
    }
}
