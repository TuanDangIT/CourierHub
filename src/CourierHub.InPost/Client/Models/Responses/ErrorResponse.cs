using System.Text.Json;

namespace CourierHub.InPost.Client.Models.Responses;

/// <summary>
/// InPost error response returned by the API.
/// </summary>
public sealed class ErrorResponse
{
    /// <summary>
    /// The HTTP status code returned by InPost.
    /// </summary>
    public required int Status { get; init; }

    /// <summary>
    /// The InPost error code.
    /// </summary>
    public required string Error { get; init; }

    /// <summary>
    /// The human-readable message.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// Validation details or other error-specific payload.
    /// </summary>
    public JsonElement? Details { get; init; }
}