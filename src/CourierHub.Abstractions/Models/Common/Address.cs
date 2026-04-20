using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Common;

/// <summary>
/// Represents a standardized address used across all courier providers.
/// Supports both separated address components (Street + House Number) and combined formats
/// to accommodate different courier API requirements.
/// </summary>
public record Address
{
    /// <summary>
    /// The street name, optionally including the street type prefix (e.g., "Wiejska" or "ul. Wiejska").
    /// </summary>
    public required string Street { get; init; }

    /// <summary>
    /// The building or house number (e.g., "4").
    /// </summary>
    public required string BuildingNumber { get; init; }

    /// <summary>
    /// The apartment or flat number (e.g., "2", "apt. 5", "m. 10").
    /// Optional for addresses without apartments (e.g., houses, commercial buildings).
    /// </summary>
    public string? ApartmentNumber { get; init; }

    /// <summary>
    /// The city or town name (e.g., "Warszawa", "Kraków").
    /// </summary>
    public required string City { get; init; }

    /// <summary>
    /// The postal code in Polish format (XX-XXX, e.g., "00-001", "31-011").
    /// </summary>
    public required string PostalCode { get; init; }

    /// <summary>
    /// The country code using ISO 3166-1 alpha-2 format (e.g., "PL", "DE", "FR").
    /// </summary>
    /// <remarks>Defaults to "PL" for Poland.</remarks>
    public string CountryCode { get; init; } = "PL";

    /// <summary>
    /// The country name (e.g., "Poland", "Germany", "France").
    /// </summary>
    public required string Country {  get; init; }
}
