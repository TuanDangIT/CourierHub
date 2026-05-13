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
    /// Gets scalar metadata with an optional default value.
    /// </summary>
    /// <param name="metadata">The metadata dictionary.</param>
    /// <param name="key">The metadata key to retrieve.</param>
    /// <param name="defaultValue">The value to return if the key is not found or is null.</param>
    /// <returns>
    /// The metadata value if the key exists; otherwise the defaultValue.
    /// </returns>
    public static string? GetScalarMetadata(
        this Dictionary<string, string?> metadata,
        string key,
        string? defaultValue = default)
    {
        ArgumentNullException.ThrowIfNull(metadata);

        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));

        return metadata.TryGetValue(key, out var value) && value != null
            ? value
            : defaultValue;
    }

    /// <summary>
    /// Checks if a metadata key exists and contains a non-null value.
    /// </summary>
    /// <param name="metadata">The metadata dictionary.</param>
    /// <param name="key">The metadata key to check.</param>
    /// <returns>
    /// true if the key exists and its value is not null; otherwise false.
    /// </returns>
    public static bool HasMetadata(
        this Dictionary<string, string?> metadata,
        string key)
    {
        ArgumentNullException.ThrowIfNull(metadata);

        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));

        return metadata.TryGetValue(key, out var value) && value != null;
    }
}
