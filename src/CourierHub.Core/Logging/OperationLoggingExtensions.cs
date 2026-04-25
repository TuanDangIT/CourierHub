using Microsoft.Extensions.Logging;

namespace CourierHub.Core.Logging;

/// <summary>
/// Provides standardized logging helpers for courier operations.
/// </summary>
public static class OperationLoggingExtensions
{
    /// <summary>
    /// Starts a logging scope for a courier operation and generates an internal operation identifier.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="operationName">The name of the operation being executed.</param>
    /// <param name="operationId">The generated operation identifier.</param>
    /// <returns>A logging scope that should be disposed when the operation ends.</returns>
    public static IDisposable? BeginOperationScope(this ILogger? logger, string operationName, out string operationId)
    {
        operationId = Guid.NewGuid().ToString("N");

        if (logger is null)
        {
            return null;
        }

        return logger.BeginScope(new Dictionary<string, object?>
        {
            ["OperationId"] = operationId,
            ["OperationName"] = operationName
        });
    }

    /// <summary>
    /// Logs that an operation has started.
    /// </summary>
    public static void LogOperationStarted(this ILogger? logger, string operationName, string operationId)
        => logger?.LogInformation("[{OperationId}] Starting {Operation}.", operationId, operationName);

    /// <summary>
    /// Logs that an operation has completed successfully.
    /// </summary>
    public static void LogOperationCompleted(this ILogger? logger, string operationName, string operationId)
        => logger?.LogInformation("[{OperationId}] Completed {Operation}.", operationId, operationName);

    /// <summary>
    /// Logs that an operation has failed.
    /// </summary>
    public static void LogOperationFailed(this ILogger? logger, Exception exception, string operationName, string operationId)
        => logger?.LogError(exception, "[{OperationId}] Failed {Operation}.", operationId, operationName);
}
