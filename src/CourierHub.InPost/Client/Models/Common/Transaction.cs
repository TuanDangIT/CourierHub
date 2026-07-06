using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents a transaction returned by the InPost API for an offer purchase.
/// </summary>
public sealed class Transaction
{
    /// <summary>
    /// The transaction identifier.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// The transaction status.
    /// </summary>
    public required string Status { get; init; }

    /// <summary>
    /// The date and time when the transaction was created.
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// The date and time when the transaction was last updated.
    /// </summary>
    public required DateTime UpdatedAt { get; init; }

    /// <summary>
    /// The identifier of the offer associated with this transaction.
    /// </summary>
    public required int OfferId { get; init; }
}
