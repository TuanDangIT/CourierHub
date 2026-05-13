using CourierHub.Abstractions.Models.Common;
using System;

namespace CourierHub.Core.Utils;

/// <summary>
/// Helpers for formatting and normalizing address values.
/// </summary>
public static class AddressFormattingUtils
{
    /// <summary>
    /// Formats a street address for courier provider requests.
    /// </summary>
    public static string GetStreetAddress(Address source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return string.IsNullOrWhiteSpace(source.ApartmentNumber)
            ? $"{source.Street} {source.BuildingNumber}"
            : $"{source.Street} {source.BuildingNumber}/{source.ApartmentNumber}";
    }

    /// <summary>
    /// Normalizes a postal code by removing the dash character.
    /// </summary>
    public static string NormalizePostalCode(string postalCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(postalCode);

        return postalCode.Replace("-", string.Empty, StringComparison.Ordinal);
    }
}
