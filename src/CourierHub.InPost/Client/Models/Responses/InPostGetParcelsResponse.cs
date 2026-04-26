namespace CourierHub.InPost.Client.Models.Responses;

/// <summary>
/// InPost shipments search/list response wrapper.
/// </summary>
internal sealed class InPostGetParcelsResponse
{
    /// <summary>
    /// The URI/href reference for this query in the InPost API.
    /// </summary>
    public required string Href { get; init; }

    /// <summary>
    /// Total number of matching items.
    /// </summary>
    public int Count { get; init; }

    /// <summary>
    /// Current page number.
    /// </summary>
    public int Page { get; init; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PerPage { get; init; }

    /// <summary>
    /// List of returned shipment items.
    /// </summary>
    public required IReadOnlyList<InPostGetParcelResponse> Items { get; init; }
}
