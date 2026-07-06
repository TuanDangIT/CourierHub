using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Requests;

/// <summary>
/// InPost shipments list/search request model.
/// </summary>
public sealed class GetParcelsRequest
{
    /// <summary>
    /// Shipment identifiers to filter by.
    /// </summary>
    public IReadOnlyList<int>? Ids { get; init; }

    /// <summary>
    /// Shipment creation date to filter by.
    /// </summary>
    public string? CreatedAt { get; init; }

    /// <summary>
    /// Lower bound for shipment creation time filter.
    /// </summary>
    public string? CreatedAtGteq { get; init; }

    /// <summary>
    /// Upper bound for shipment creation time filter.
    /// </summary>
    public string? CreatedAtLteq { get; init; }

    /// <summary>
    /// Tracking number filter.
    /// </summary>
    public string? TrackingNumber { get; init; }

    /// <summary>
    /// Tracking number contains filter.
    /// </summary>
    public string? TrackingNumberCont { get; init; }

    /// <summary>
    /// Shipment statuses to filter by.
    /// </summary>
    public IReadOnlyList<string>? Status { get; init; }

    /// <summary>
    /// Shipment services to filter by.
    /// </summary>
    public IReadOnlyList<string>? Service { get; init; }

    /// <summary>
    /// Shipment carriers to filter by.
    /// </summary>
    public IReadOnlyList<string>? Carrier { get; init; }

    /// <summary>
    /// Lower bound for insurance amount filter.
    /// </summary>
    public decimal? InsuranceAmountGteq { get; init; }

    /// <summary>
    /// Upper bound for insurance amount filter.
    /// </summary>
    public decimal? InsuranceAmountLteq { get; init; }

    /// <summary>
    /// Lower bound for COD amount filter.
    /// </summary>
    public decimal? CodAmountGteq { get; init; }

    /// <summary>
    /// Upper bound for COD amount filter.
    /// </summary>
    public decimal? CodAmountLteq { get; init; }

    /// <summary>
    /// Receiver name prefix filter.
    /// </summary>
    public string? ReceiverName { get; init; }

    /// <summary>
    /// Receiver address prefix filter.
    /// </summary>
    public string? ReceiverAddress { get; init; }

    /// <summary>
    /// Receiver city filter.
    /// </summary>
    public string? ReceiverCity { get; init; }

    /// <summary>
    /// Receiver post code prefix filter.
    /// </summary>
    public string? ReceiverPostCode { get; init; }

    /// <summary>
    /// Receiver country code filter.
    /// </summary>
    public string? ReceiverCountryCode { get; init; }

    /// <summary>
    /// Receiver phone filter.
    /// </summary>
    public string? ReceiverPhone { get; init; }

    /// <summary>
    /// Receiver email prefix filter.
    /// </summary>
    public string? ReceiverEmail { get; init; }

    /// <summary>
    /// Sender name prefix filter.
    /// </summary>
    public string? SenderName { get; init; }

    /// <summary>
    /// Sender address prefix filter.
    /// </summary>
    public string? SenderAddress { get; init; }

    /// <summary>
    /// Sender city filter.
    /// </summary>
    public string? SenderCity { get; init; }

    /// <summary>
    /// Sender post code prefix filter.
    /// </summary>
    public string? SenderPostCode { get; init; }

    /// <summary>
    /// Sender country code filter.
    /// </summary>
    public string? SenderCountryCode { get; init; }

    /// <summary>
    /// Sender phone filter.
    /// </summary>
    public string? SenderPhone { get; init; }

    /// <summary>
    /// Sender email prefix filter.
    /// </summary>
    public string? SenderEmail { get; init; }

    /// <summary>
    /// Monitoring flag filter.
    /// </summary>
    public bool? Monitoring { get; init; }

    /// <summary>
    /// External customer identifier filter.
    /// </summary>
    public string? ExternalCustomerId { get; init; }

    /// <summary>
    /// Shipment sending methods to filter by.
    /// </summary>
    public IReadOnlyList<string>? SendingMethod { get; init; }

    /// <summary>
    /// Only choice active offers filter.
    /// </summary>
    public bool? OnlyChoiceActiveOffers { get; init; }

    /// <summary>
    /// Offers status filter.
    /// </summary>
    public string? OffersStatus { get; init; }

    /// <summary>
    /// Number of the page to retrieve.
    /// </summary>
    public int? Page { get; init; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int? PerPage { get; init; }

    /// <summary>
    /// Sort field.
    /// </summary>
    public string? SortBy { get; init; }

    /// <summary>
    /// Sort order.
    /// </summary>
    public string? SortOrder { get; init; }
}
