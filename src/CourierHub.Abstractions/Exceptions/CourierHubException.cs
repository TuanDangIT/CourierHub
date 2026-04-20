namespace CourierHub.Abstractions.Exceptions;

/// <summary>
/// Represents the base exception for all CourierHub-specific errors.
/// </summary>
/// <remarks>
/// Use this exception as the common parent for all custom exceptions thrown by the CourierHub library.
/// Consumers can catch <see cref="CourierHubException"/> to handle all library-specific failures in one place.
/// </remarks>
public abstract class CourierHubException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CourierHubException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    protected CourierHubException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CourierHubException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    protected CourierHubException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
