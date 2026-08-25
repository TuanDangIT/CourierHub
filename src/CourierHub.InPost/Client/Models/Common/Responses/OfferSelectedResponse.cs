using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common.Responses;

/// <summary>
/// Represents an offer item returned in the InPost payment response.
/// </summary>
public sealed class OfferSelectedResponse
{
    /// <summary>
    /// Offer identifier.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Service details for the offer.
    /// </summary>
    public required Service Service { get; init; }

    /// <summary>
    /// Carrier details for the offer.
    /// </summary>
    public required Carrier Carrier { get; init; }

    /// <summary>
    /// Additional services included in the offer.
    /// </summary>
    public IReadOnlyList<string> AdditionalServices { get; init; } = [];

    /// <summary>
    /// Offer status.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// Expiration date of the offer.
    /// </summary>
    public DateTimeOffset? ValidTo { get; init; }

    /// <summary>
    /// Offer price.
    /// </summary>
    public decimal? Rate { get; init; }

    /// <summary>
    /// Currency code.
    /// </summary>
    public required string Currency { get; init; }

    /// <summary>
    /// Reasons why the offer is unavailable, if any.
    /// </summary>
    public IReadOnlyList<string> UnavailabilityReasons { get; init; } = [];
}
