using CourierHub.Core.DependencyInjection;
using CourierHub.InPost.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;

namespace CourierHub.InPost.DependencyInjection;

/// <summary>
/// Fluent registration extensions for InPost provider.
/// </summary>
public static class InPostCourierSettingsBuilderExtensions
{
    /// <summary>
    /// Registers InPost provider and configuration.
    /// </summary>
    /// <param name="builder">Courier settings builder.</param>
    /// <param name="setupAction">Action used to configure InPost options.</param>
    /// <returns>The same builder instance for fluent chaining.</returns>
    public static CourierHubSettingsBuilder AddInPost(this CourierHubSettingsBuilder builder, Action<InPostOptions> setupAction)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(setupAction);

        var inPostOptions = new InPostOptions
        {
            OrganizationId = string.Empty,
            ApiKey = string.Empty,
            BaseUrl = string.Empty
        };
        setupAction(inPostOptions);

        if (string.IsNullOrWhiteSpace(inPostOptions.OrganizationId))
        {
            throw new ArgumentException("InPost OrganizationId cannot be null or empty.", nameof(setupAction));
        }

        if (string.IsNullOrWhiteSpace(inPostOptions.ApiKey))
        {
            throw new ArgumentException("InPost ApiKey cannot be null or empty.", nameof(setupAction));
        }

        if (string.IsNullOrWhiteSpace(inPostOptions.BaseUrl))
        {
            throw new ArgumentException("InPost BaseUrl cannot be null or empty.", nameof(setupAction));
        }

        builder.Services.AddSingleton(inPostOptions);

        var sharedHttpOptions = builder.Http;
        builder.Services
            .AddHttpClient<IInpostCourierProvider, InPostCourierProvider>((sp, httpClient) =>
            {
                httpClient.Timeout = sharedHttpOptions.Timeout;
                httpClient.BaseAddress = new Uri(sp.GetRequiredService<InPostOptions>().BaseUrl, UriKind.Absolute);
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
