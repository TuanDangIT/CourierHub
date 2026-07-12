using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using CourierHub.Core.Validation;
using System.Linq;
using System.Collections.Generic;
using CourierHub.Core.Errors;
using CourierHub.Core.Exceptions;
using CourierHub.Core.Result;

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

    /// <summary>
    /// Validates a request object implementing <see cref="IValidatable"/>.
    /// </summary>
    /// <param name="request">The object to validate.</param>
    protected Result.Result Validate(IValidatable request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var errors = request.Validate();
        if (errors is null || errors.Count == 0) return Result.Result.Success();

        return Result.Result.Failure(errors);
    }
}
