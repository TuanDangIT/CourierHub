using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents a InPost specific address format used for sender and recipient information in the InPost API.
/// </summary>
internal sealed class InPostAddress
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
    public required string PostCode { get; init; }

    /// <summary>
    /// The country code using ISO 3166-1 alpha-2 format (e.g., "PL", "DE", "FR").
    /// </summary>
    public required string CountryCode { get; init; }
}
