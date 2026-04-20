using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace CourierHub.Core.Base;

/// <summary>
/// Provides a base class for HTTP clients that interact with courier APIs.
/// </summary>
/// <remarks>
/// Handles JSON serialization/deserialization using Source Generators for AOT compatibility,
/// error handling, and structured logging. Designed for use in both Dependency Injection
/// and manual instantiation scenarios.
/// </remarks>
public abstract class HttpClientBase
{
    /// <summary>
    /// Provides access to the HTTP client.
    /// </summary>
    protected readonly HttpClient _httpClient;

    /// <summary>
    /// Provides access to the logger instance used for recording diagnostic and operational messages within the class.
    /// </summary>
    protected readonly ILogger? _logger;

    /// <summary>
    /// Initializes a new instance of the HttpClientBase class.
    /// </summary>
    /// <param name="httpClient">The HTTP client used for API communication. Required.</param>
    /// <param name="logger">The logger instance for diagnostic output. If null, uses NullLogger (no-op logging).</param>
    /// <remarks>
    /// The logger parameter is optional to support both Dependency Injection scenarios
    /// (where a logger is injected) and manual instantiation (where a logger may not be available).
    /// </remarks>
    protected HttpClientBase(HttpClient httpClient, ILogger? logger = default)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    /// <summary>
    /// Sends a POST request and deserializes the response using Source Generators.
    /// </summary>
    /// <typeparam name="TRequest">The request payload type.</typeparam>
    /// <typeparam name="TResponse">The response payload type.</typeparam>
    /// <param name="url">The API endpoint URL.</param>
    /// <param name="request">The request payload to send.</param>
    /// <param name="requestTypeInfo">Source-generated TypeInfo for the request type (AOT support).</param>
    /// <param name="responseTypeInfo">Source-generated TypeInfo for the response type (AOT support).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The deserialized API response.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the API returns an empty response or if the request fails.</exception>
    protected async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request, JsonTypeInfo<TRequest> requestTypeInfo, JsonTypeInfo<TResponse> responseTypeInfo, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync(url, request, requestTypeInfo, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger?.LogError("API Error at {Url}: {Status} - {Content}", url, response.StatusCode, errorContent);
            response.EnsureSuccessStatusCode();
        }

        var result = await response.Content.ReadFromJsonAsync(responseTypeInfo, cancellationToken);

        return result ?? throw new InvalidOperationException("API returned an empty response.");
    }

    /// <summary>
    /// Sends a GET request and deserializes the response using Source Generators.
    /// </summary>
    /// <typeparam name="TResponse">The response payload type.</typeparam>
    /// <param name="url">The API endpoint URL.</param>
    /// <param name="responseTypeInfo">Source-generated TypeInfo for the response type (AOT support).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The deserialized API response.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the API returns an empty response.</exception>
    protected async Task<TResponse> GetAsync<TResponse>(string url, JsonTypeInfo<TResponse> responseTypeInfo, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync(url, responseTypeInfo, cancellationToken);
        return result ?? throw new InvalidOperationException("API returned an empty response.");
    }
}