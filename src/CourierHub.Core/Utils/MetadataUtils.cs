using System;
using System.Collections.Generic;

namespace CourierHub.Core.Utils;

/// <summary>
/// Helpers for extracting provider-specific values from metadata dictionaries.
/// </summary>
public static class MetadataUtils
{
    /// <summary>
    /// Gets a required metadata value by exact key.
    /// </summary>
    public static T GetRequiredMetadataValue<T>(IReadOnlyDictionary<string, object> metadata, string key)
    {
        var value = GetMetadataValue<T>(metadata, key) ?? throw new ArgumentException($"Missing required metadata value '{key}'.", nameof(metadata));

        if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
        {
            throw new ArgumentException($"Missing required metadata value '{key}'.", nameof(metadata));
        }

        return value;
    }

    /// <summary>
    /// Gets an optional metadata value by exact key.
    /// </summary>
    public static T? GetMetadataValue<T>(IReadOnlyDictionary<string, object> metadata, string key)
        => metadata.TryGetValue(key, out var value) && value is T typedValue ? typedValue : default;
}
