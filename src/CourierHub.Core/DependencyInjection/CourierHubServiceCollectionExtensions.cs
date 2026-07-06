using Microsoft.Extensions.DependencyInjection;

namespace CourierHub.Core.DependencyInjection;

/// <summary>
/// Extensions for registering CourierHub services.
/// </summary>
public static class CourierHubServiceCollectionExtensions
{
    /// <summary>
    /// Adds CourierHub infrastructure and configures courier providers using a fluent builder.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    /// <param name="configure">Builder callback used to register providers.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddCourierHub(this IServiceCollection services, Action<CourierHubSettingsBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        var builder = new CourierHubSettingsBuilder(services);
        configure(builder);

        return services;
    }
}
