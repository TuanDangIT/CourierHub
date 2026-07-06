using CourierHub.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using System.Buffers.Text;

namespace CourierHub.Core.Http;

/// <summary>
/// Creates configured HTTP clients for CourierHub scenarios.
/// </summary>
public static class CourierHubHttpClientFactory
{
    /// <summary>
    /// Creates and configures an <see cref="HttpClient"/> instance using the supplied HTTP options.
    /// </summary>
    /// <param name="configure">Optional callback used to configure the HTTP options before client creation.</param>
    /// <returns></returns>
    public static HttpClient CreateHttpClient(Action<HttpOptions>? configure = default)
    {
        var options = new HttpOptions();
        configure?.Invoke(options);

        var pipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                MaxRetryAttempts = options.Retry.MaxRetryAttempts,
                Delay = options.Retry.Delay,
                MaxDelay = options.Retry.MaxDelay,
                UseJitter = options.Retry.UseJitter,
            })
            .Build();

        var socketsHandler = new SocketsHttpHandler();
        if (options.PooledConnectionLifetime is { } lifetime)
            socketsHandler.PooledConnectionLifetime = lifetime;

        return new HttpClient(new ResiliencePipelineHandler(pipeline, socketsHandler))
        {
            Timeout = options.Timeout
        };
    }
}

/// <summary>
/// A <see cref="DelegatingHandler"/> that executes every HTTP request
/// </summary>
/// <param name="pipeline">The resilience pipeline to execute.</param>
/// <param name="innerHandler">The inner HTTP message handler.</param>
internal sealed class ResiliencePipelineHandler(ResiliencePipeline<HttpResponseMessage> pipeline, HttpMessageHandler innerHandler) : DelegatingHandler(innerHandler)
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
        => pipeline.ExecuteAsync(
            async ct => await base.SendAsync(request, ct),
            cancellationToken).AsTask();
}
