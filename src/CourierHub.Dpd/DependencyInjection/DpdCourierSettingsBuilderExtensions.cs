using CourierHub.Abstractions.Interfaces;
using CourierHub.Core.Configuration;
using CourierHub.Dpd.Configurations;
using CourierHub.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
    /// <param name="resilienceSetupAction">Optional action used to configure HTTP resilience settings for Dpd.</param>
    /// <returns>The same builder instance for fluent chaining.</returns>
    public static CourierSettingsBuilder AddDpd(
        this CourierSettingsBuilder builder,
        Action<DpdOptions> setupAction,
        Action<HttpResilienceOptions>? resilienceSetupAction = default)
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

        var resilienceOptions = new HttpResilienceOptions();
        resilienceSetupAction?.Invoke(resilienceOptions);

        builder.Services.AddSingleton(dpdOptions);

        builder.Services.AddHttpClient("CourierHub.Dpd");

        builder.Services.AddScoped<ICourierProvider>(sp =>
            new DpdCourierProvider(
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("CourierHub.Dpd"),
                sp.GetRequiredService<DpdOptions>(),
                resilienceOptions,
                sp.GetService<ILogger<DpdCourierProvider>>()));

        return builder;
    }
}
