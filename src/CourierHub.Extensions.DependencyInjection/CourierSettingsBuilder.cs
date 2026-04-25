using Microsoft.Extensions.DependencyInjection;

namespace CourierHub.Extensions.DependencyInjection;

/// <summary>
/// Fluent builder for registering courier providers.
/// </summary>
public sealed class CourierSettingsBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CourierSettingsBuilder"/> class.
    /// </summary>
    /// <param name="services">The service collection used for registrations.</param>
    public CourierSettingsBuilder(IServiceCollection services)
    {
        Services = services;
    }

    /// <summary>
    /// Gets the DI service collection.
    /// </summary>
    public IServiceCollection Services { get; }
}
