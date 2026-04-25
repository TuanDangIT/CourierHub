using CourierHub.Abstractions.Interfaces;
using CourierHub.Core.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CourierHub.Extensions.DependencyInjection;

/// <summary>
/// Extensions for registering CourierHub services.
/// </summary>
public static class CourierServiceCollectionExtensions
{
    /// <summary>
    /// Adds CourierHub infrastructure and configures courier providers using a fluent builder.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    /// <param name="configure">Builder callback used to register providers.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddCourierService(this IServiceCollection services, Action<CourierSettingsBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        services.TryAddScoped<ICourierProviderFactory, CourierProviderFactory>();

        var builder = new CourierSettingsBuilder(services);
        configure(builder);

        return services;
    }
}
