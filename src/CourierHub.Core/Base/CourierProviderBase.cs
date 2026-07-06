using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Core.Base;

/// <summary>
/// Provides a base implementation for courier service providers.
/// </summary>
public abstract class CourierProviderBase 
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CourierProviderBase"/> class with the specified logger.
    /// </summary>
    /// <param name="loggerFactory">The logger factory for creating loggers.</param>
    protected CourierProviderBase(ILoggerFactory? loggerFactory = default)
    {
    }
}
