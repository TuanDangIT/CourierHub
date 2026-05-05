using System;
using System.Collections.Generic;

namespace CourierHub.Abstractions.Extensions;

/// <summary>
/// Extension methods for accessing provider-specific metadata from response objects.
/// </summary>
/// <remarks>
/// These extensions provide a convenient and type-safe way to access metadata stored in the 
/// response dictionary, allowing consumers to access provider-specific information without 
/// direct dependencies on provider implementations.
/// </remarks>
public static class MetadataExtensions
{
    /// <summary>
    /// Safely retrieves provider-specific metadata as a strongly-typed object.
    /// </summary>
    /// <typeparam name="T">The expected type of the metadata value.</typeparam>
    /// <param name="metadata">The metadata dictionary.</param>
    /// <param name="key">The metadata key to retrieve.</param>
    /// <returns>
    /// The metadata value cast to type T if the key exists and the value is of type T; 
    /// otherwise null.
    /// </returns>
    /// <remarks>
    /// This method is useful when metadata contains provider-specific objects or DTOs.
    /// The consumer is responsible for knowing the correct type T.
    /// 
    /// Example:
    /// <code>
    /// var inPostSender = response.Metadata.GetProviderMetadata&lt;InPostSender&gt;("InPost_Sender");
    /// </code>
    /// </remarks>
    public static T? GetProviderMetadata<T>(
        this Dictionary<string, object?> metadata,
        string key)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(metadata);

        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));

        return metadata.TryGetValue(key, out var value) ? value as T : null;
    }

    /// <summary>
    /// Retrieves nested dictionary metadata (for accessing complex structured data).
    /// </summary>
    /// <param name="metadata">The metadata dictionary.</param>
    /// <param name="key">The metadata key to retrieve.</param>
    /// <returns>
    /// A nested dictionary containing the metadata if the key exists and the value is a dictionary; 
    /// otherwise null.
    /// </returns>
    /// <remarks>
    /// This method is useful when metadata contains complex objects that were serialized to dictionaries.
    /// 
    /// Example:
    /// <code>
    /// var sender = response.Metadata.GetMetadataAsDict("InPost_Sender");
    /// if (sender != null)
    /// {
    ///     var firstName = sender["FirstName"]?.ToString();
    ///     var address = sender["Address"] as Dictionary&lt;string, object&gt;;
    /// }
    /// </code>
    /// </remarks>
    public static Dictionary<string, object>? GetMetadataAsDict(
        this Dictionary<string, object?> metadata,
        string key)
    {
        ArgumentNullException.ThrowIfNull(metadata);

        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));

        return metadata.TryGetValue(key, out var value)
            ? value as Dictionary<string, object>
            : null;
    }

    /// <summary>
    /// Gets scalar metadata with an optional default value.
    /// </summary>
    /// <typeparam name="T">The expected type of the metadata value.</typeparam>
    /// <param name="metadata">The metadata dictionary.</param>
    /// <param name="key">The metadata key to retrieve.</param>
    /// <param name="defaultValue">The value to return if the key is not found or is null.</param>
    /// <returns>
    /// The metadata value cast to type T if the key exists and the value is of type T; 
    /// otherwise the defaultValue.
    /// </returns>
    /// <remarks>
    /// This method is useful for retrieving simple scalar values like strings, numbers, or dates.
    /// 
    /// Example:
    /// <code>
    /// var returnTracking = response.Metadata.GetScalarMetadata&lt;string&gt;("InPost_ReturnTrackingNumber", "N/A");
    /// var createdAt = response.Metadata.GetScalarMetadata&lt;DateTime&gt;("InPost_CreatedAt");
    /// </code>
    /// </remarks>
    public static T? GetScalarMetadata<T>(
        this Dictionary<string, object?> metadata,
        string key,
        T? defaultValue = default)
    {
        ArgumentNullException.ThrowIfNull(metadata);

        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));

        if (metadata.TryGetValue(key, out var value) && value != null)
        {
            return value is T typedValue ? typedValue : defaultValue;
        }

        return defaultValue;
    }

    /// <summary>
    /// Checks if a metadata key exists and contains a non-null value.
    /// </summary>
    /// <param name="metadata">The metadata dictionary.</param>
    /// <param name="key">The metadata key to check.</param>
    /// <returns>
    /// true if the key exists and its value is not null; otherwise false.
    /// </returns>
    /// <remarks>
    /// Example:
    /// <code>
    /// if (response.Metadata.HasMetadata("InPost_Sender"))
    /// {
    ///     var sender = response.Metadata.GetMetadataAsDict("InPost_Sender");
    /// }
    /// </code>
    /// </remarks>
    public static bool HasMetadata(
        this Dictionary<string, object?> metadata,
        string key)
    {
        ArgumentNullException.ThrowIfNull(metadata);

        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));

        return metadata.TryGetValue(key, out var value) && value != null;
    }
}
