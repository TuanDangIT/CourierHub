using System.Globalization;

namespace CourierHub.Core.Utils;

/// <summary>
/// Utility helpers for building query string parameters.
/// </summary>
public static class QueryStringUtils
{
    /// <summary>
    /// Appends a string query parameter when the value is not null or whitespace.
    /// </summary>
    /// <param name="query">The query parameter collection.</param>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    public static void Append(ICollection<string> query, string key, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            query.Add($"{key}={Uri.EscapeDataString(value)}");
        }
    }

    /// <summary>
    /// Appends an integer query parameter when the value is present.
    /// </summary>
    /// <param name="query">The query parameter collection.</param>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    public static void Append(ICollection<string> query, string key, int? value)
    {
        if (value is not null)
        {
            query.Add($"{key}={value.Value.ToString(CultureInfo.InvariantCulture)}");
        }
    }

    /// <summary>
    /// Appends a decimal query parameter when the value is present.
    /// </summary>
    /// <param name="query">The query parameter collection.</param>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    public static void Append(ICollection<string> query, string key, decimal? value)
    {
        if (value is not null)
        {
            query.Add($"{key}={value.Value.ToString(CultureInfo.InvariantCulture)}");
        }
    }

    /// <summary>
    /// Appends a boolean query parameter when the value is present.
    /// </summary>
    /// <param name="query">The query parameter collection.</param>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    public static void Append(ICollection<string> query, string key, bool? value)
    {
        if (value is not null)
        {
            query.Add($"{key}={value.Value.ToString().ToLowerInvariant()}");
        }
    }

    /// <summary>
    /// Appends a comma-separated query parameter when the values collection contains items.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    /// <param name="query">The query parameter collection.</param>
    /// <param name="key">The parameter name.</param>
    /// <param name="values">The parameter values.</param>
    public static void Append<T>(ICollection<string> query, string key, IEnumerable<T>? values)
    {
        if (values is null)
        {
            return;
        }

        var items = values
            .Where(static value => value is not null)
            .Select(static value => Uri.EscapeDataString(value!.ToString()!))
            .ToArray();

        if (items.Length > 0)
        {
            query.Add($"{key}={string.Join(',', items)}");
        }
    }
}
