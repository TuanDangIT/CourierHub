using CourierHub.Core.Base;

namespace CourierHub.Core.Configuration;

/// <summary>
/// Represents retry and timeout resilience settings used by <see cref="HttpClientBase"/>.
/// </summary>
public sealed class HttpResilienceOptions
{
    /// <summary>
    /// Enables or disables resilience behavior.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Maximum number of retry attempts for transient failures.
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Base delay used for exponential backoff.
    /// </summary>
    public TimeSpan BaseDelay { get; set; } = TimeSpan.FromMilliseconds(250);

    /// <summary>
    /// Maximum delay cap for exponential backoff.
    /// </summary>
    public TimeSpan MaxDelay { get; set; } = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Adds random jitter to retry delays.
    /// </summary>
    public bool UseJitter { get; set; } = true;
}
