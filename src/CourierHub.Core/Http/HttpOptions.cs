using Microsoft.Extensions.Http.Resilience;

namespace CourierHub.Core.Http;

/// <summary>
/// Central HTTP configuration used by all courier providers.
/// </summary>
public sealed class HttpOptions
{
    /// <summary>
    /// The request timeout applied to the created <see cref="HttpClient"/>.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

    /// <summary>
    /// The pooled connection lifetime for the underlying handler.
    /// </summary>
    public TimeSpan? PooledConnectionLifetime { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// The built-in retry configuration used by the resilience pipeline.
    /// </summary>
    public HttpRetryStrategyOptions Retry { get; } = new()
    {
        MaxRetryAttempts = 3,
        Delay = TimeSpan.FromSeconds(2),
        MaxDelay = TimeSpan.FromSeconds(30),
        UseJitter = true
    };
}
