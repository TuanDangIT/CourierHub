using CourierHub.Abstractions.Interfaces;
using CourierHub.Core.Configuration;
using CourierHub.Extensions.DependencyInjection;
using CourierHub.InPost.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
    /// <param name="resilienceSetupAction">Optional action used to configure HTTP resilience settings for InPost.</param>
    /// <returns>The same builder instance for fluent chaining.</returns>
    public static CourierSettingsBuilder AddInPost(
        this CourierSettingsBuilder builder,
        Action<InPostOptions> setupAction,
        Action<HttpResilienceOptions>? resilienceSetupAction = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(setupAction);

        var inPostOptions = new InPostOptions
        {
            OrganizationId = string.Empty,
            ApiKey = string.Empty
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

        var resilienceOptions = new HttpResilienceOptions();
        resilienceSetupAction?.Invoke(resilienceOptions);

        builder.Services.AddSingleton(inPostOptions);
        builder.Services.AddSingleton(resilienceOptions);

        builder.Services.AddHttpClient("CourierHub.InPost");

        builder.Services.AddScoped<ICourierProvider>(sp =>
            new InPostCourierProvider(
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("CourierHub.InPost"),
                sp.GetRequiredService<InPostOptions>(),
                sp.GetRequiredService<HttpResilienceOptions>(),
                sp.GetService<ILogger<InPostCourierProvider>>()));

        return builder;
    }
}
