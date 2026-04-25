using CourierHub.Core.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace CourierHub.Core.Base;

/// <summary>
/// Provides a base class for HTTP clients that interact with courier APIs.
/// </summary>
/// <remarks>
/// Handles JSON serialization/deserialization using Source Generators for AOT compatibility,
/// error handling, and configurable resilience for transient HTTP failures.
/// Designed for use in both Dependency Injection and manual instantiation scenarios.
/// </remarks>
public abstract class HttpClientBase
{
    /// <summary>
    /// Retryable HTTP status codes that indicate transient failures where retrying the request may succeed.
    /// </summary>
    private static readonly HttpStatusCode[] RetryableStatusCode =
    [
        HttpStatusCode.RequestTimeout,
        HttpStatusCode.TooManyRequests,
        HttpStatusCode.BadGateway,
        HttpStatusCode.ServiceUnavailable,
        HttpStatusCode.GatewayTimeout
    ];

    /// <summary>
    /// Provides access to the HTTP client.
    /// </summary>
    protected readonly HttpClient _httpClient;

    /// <summary>
    /// Provides access to the logger instance used for recording diagnostic and operational messages within the class.
    /// </summary>
    protected readonly ILogger? _logger;

    /// <summary>
    /// Resilience options that control retry behavior for transient HTTP failures. If null, default settings are used.
    /// </summary>
    private readonly HttpResilienceOptions _resilienceOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientBase"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client used for API communication. Required.</param>
    /// <param name="logger">Optional logger instance.</param>
    /// <param name="resilienceOptions">Optional resilience settings. If null, default settings are used.</param>
    protected HttpClientBase(HttpClient httpClient, ILogger? logger = null, HttpResilienceOptions? resilienceOptions = null)
    {
        _httpClient = httpClient;
        _logger = logger;
        _resilienceOptions = resilienceOptions ?? new HttpResilienceOptions();
    }

    /// <summary>
    /// Sends a POST request and deserializes the response using Source Generators.
    /// Applies transient retry behavior based on configured resilience options.
    /// </summary>
    /// <typeparam name="TRequest">The request payload type.</typeparam>
    /// <typeparam name="TResponse">The expected response payload type.</typeparam>
    /// <param name="url">The relative or absolute endpoint URL.</param>
    /// <param name="request">The request body to serialize and send as JSON.</param>
    /// <param name="requestTypeInfo">Source-generated JSON metadata for <typeparamref name="TRequest"/>.</param>
    /// <param name="responseTypeInfo">Source-generated JSON metadata for <typeparamref name="TResponse"/>.</param>
    /// <param name="configureRequest">Optional callback for customizing the outgoing HTTP request (for example, adding per-request headers).</param>
    /// <param name="cancellationToken">A token to cancel the HTTP operation.</param>
    /// <returns>The deserialized response payload.</returns>
    /// <exception cref="HttpRequestException">Thrown when the HTTP response indicates a non-success status code.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the API returns an empty response body.</exception>
    protected async Task<TResponse> PostAsync<TRequest, TResponse>(
        string url,
        TRequest request,
        JsonTypeInfo<TRequest> requestTypeInfo,
        JsonTypeInfo<TResponse> responseTypeInfo,
        Action<HttpRequestMessage>? configureRequest = null,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteWithResilienceAsync(async cancellationToken =>
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(request, requestTypeInfo)
            };

            configureRequest?.Invoke(httpRequest);

            _logger?.LogDebug("Sending {Method} request to {Url}.", httpRequest.Method, url);

            using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

            _logger?.LogDebug("Received {StatusCode} from {Url}.", (int)response.StatusCode, url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger?.LogError("API Error at {Url}: {Status} - {Content}", url, response.StatusCode, errorContent);
                response.EnsureSuccessStatusCode();
            }

            var result = await response.Content.ReadFromJsonAsync(responseTypeInfo, cancellationToken);
            return result ?? throw new InvalidOperationException("API returned an empty response.");
        }, cancellationToken);
    }

    /// <summary>
    /// Sends a GET request and deserializes the response using Source Generators.
    /// Applies transient retry behavior based on configured resilience options.
    /// </summary>
    /// <typeparam name="TResponse">The expected response payload type.</typeparam>
    /// <param name="url">The relative or absolute endpoint URL.</param>
    /// <param name="responseTypeInfo">Source-generated JSON metadata for <typeparamref name="TResponse"/>.</param>
    /// <param name="configureRequest">Optional callback for customizing the outgoing HTTP request (for example, adding per-request headers).</param>
    /// <param name="cancellationToken">A token to cancel the HTTP operation.</param>
    /// <returns>The deserialized response payload.</returns>
    /// <exception cref="HttpRequestException">Thrown when the HTTP response indicates a non-success status code.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the API returns an empty response body.</exception>
    protected async Task<TResponse> GetAsync<TResponse>(
        string url,
        JsonTypeInfo<TResponse> responseTypeInfo,
        Action<HttpRequestMessage>? configureRequest = null,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteWithResilienceAsync(async cancellationToken =>
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            configureRequest?.Invoke(httpRequest);

            _logger?.LogDebug("Sending {Method} request to {Url}.", httpRequest.Method, url);

            using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

            _logger?.LogDebug("Received {StatusCode} from {Url}.", (int)response.StatusCode, url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger?.LogError("API Error at {Url}: {Status} - {Content}", url, response.StatusCode, errorContent);
                response.EnsureSuccessStatusCode();
            }

            var result = await response.Content.ReadFromJsonAsync(responseTypeInfo, cancellationToken);
            return result ?? throw new InvalidOperationException("API returned an empty response.");
        }, cancellationToken);
    }

    /// <summary>
    /// Sends a GET request and returns the response body as raw bytes.
    /// Applies transient retry behavior based on configured resilience options.
    /// </summary>
    /// <param name="url">The relative or absolute endpoint URL.</param>
    /// <param name="configureRequest">Optional callback for customizing the outgoing HTTP request (for example, adding per-request headers).</param>
    /// <param name="cancellationToken">A token to cancel the HTTP operation.</param>
    /// <returns>The response body as a byte array.</returns>
    /// <exception cref="HttpRequestException">Thrown when the HTTP response indicates a non-success status code.</exception>
    protected async Task<byte[]> GetAsync(
        string url,
        Action<HttpRequestMessage>? configureRequest = null,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteWithResilienceAsync(async cancellationToken =>
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            configureRequest?.Invoke(httpRequest);

            _logger?.LogDebug("Sending {Method} request to {Url}.", httpRequest.Method, url);

            using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

            _logger?.LogDebug("Received {StatusCode} from {Url}.", (int)response.StatusCode, url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger?.LogError("API Error at {Url}: {Status} - {Content}", url, response.StatusCode, errorContent);
                response.EnsureSuccessStatusCode();
            }

            return await response.Content.ReadAsByteArrayAsync(cancellationToken);
        }, cancellationToken);
    }

    /// <summary>
    /// Execute the provided action with resilience, applying retry logic for transient HTTP failures based on the configured options.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the action.</typeparam>
    /// <param name="action">Action to execute with resilience.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The result of the action.</returns>
    private async Task<T> ExecuteWithResilienceAsync<T>(Func<CancellationToken, Task<T>> action, CancellationToken cancellationToken)
    {
        if (!_resilienceOptions.Enabled)
        {
            return await action(cancellationToken);
        }

        for (var attempt = 0; ; attempt++)
        {
            try
            {
                return await action(cancellationToken);
            }
            catch (Exception ex) when (ShouldRetry(ex, attempt, cancellationToken))
            {
                var delay = GetRetryDelay(attempt);

                _logger?.LogWarning(
                    ex,
                    "Transient HTTP failure. Retrying in {DelayMs} ms. Attempt {Attempt}/{MaxRetries}",
                    delay.TotalMilliseconds,
                    attempt + 1,
                    _resilienceOptions.MaxRetries);

                await Task.Delay(delay, cancellationToken);
            }
        }
    }

    /// <summary>
    /// Determines whether the operation should be retried based on the exception type, HTTP status code, attempt count, and cancellation token state.
    /// </summary>
    /// <param name="exception">The exception that occurred during the operation.</param>
    /// <param name="attempt">The current attempt count.</param>
    /// <param name="cancellationToken">The cancellation token to observe.</param>
    /// <returns>True if the operation should be retried; otherwise, false.</returns>
    private bool ShouldRetry(Exception exception, int attempt, CancellationToken cancellationToken)
    {
        if (attempt >= _resilienceOptions.MaxRetries)
        {
            return false;
        }

        return exception switch
        {
            HttpRequestException httpEx when httpEx.StatusCode is null => true,
            HttpRequestException httpEx when httpEx.StatusCode is HttpStatusCode status && RetryableStatusCode.Contains(status) => true,
            TaskCanceledException when !cancellationToken.IsCancellationRequested => true,
            _ => false
        };
    }

    /// <summary>
    /// Calculates the delay before the next retry attempt using exponential backoff with optional jitter, based on the configured resilience options.  
    /// </summary>
    /// <param name="attempt">The current attempt count.</param>
    /// <returns></returns>
    private TimeSpan GetRetryDelay(int attempt)
    {
        var delayMs = _resilienceOptions.BaseDelay.TotalMilliseconds * Math.Pow(2, attempt);
        delayMs = Math.Min(delayMs, _resilienceOptions.MaxDelay.TotalMilliseconds);

        if (_resilienceOptions.UseJitter)
        {
            delayMs += Random.Shared.Next(0, 150);
        }

        return TimeSpan.FromMilliseconds(delayMs);
    }
}