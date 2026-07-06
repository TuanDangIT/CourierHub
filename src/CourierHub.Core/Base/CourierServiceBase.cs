using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CourierHub.Core.Base;

/// <summary>
/// Base class for courier service implementations. Provides logging helpers for service operations.
/// </summary>
public abstract class CourierServiceBase
{
    /// <summary>
    /// Logger instance available to derived service classes.
    /// </summary>
    protected readonly ILogger? _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CourierServiceBase"/> class.
    /// </summary>
    /// <param name="logger">Optional logger to be used by services.</param>
    protected CourierServiceBase(ILogger? logger = default)
    {
        _logger = logger;
    }
}
