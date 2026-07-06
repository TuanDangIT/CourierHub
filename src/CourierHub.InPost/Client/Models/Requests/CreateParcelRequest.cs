using CourierHub.InPost.Client.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CourierHub.InPost.Client.Models.Requests;

/// <summary>
/// InPost specific request model for creating a new parcel. This class encapsulates all the necessary information required by InPost to process a parcel creation request, including sender and receiver details, parcel information, service code, and additional attributes specific to InPost's requirements.
/// </summary>
public sealed class CreateParcelRequest
{
    /// <summary>
    /// The party sending the parcel (shipper).
    /// </summary>
    public required Peer Sender { get; init; }

    /// <summary>
    /// The party receiving the parcel (recipient).
    /// </summary>
    public required Peer Receiver { get; init; }

    /// <summary>
    /// The list of parcels to be shipped. Each parcel includes its dimensions and weight. A shipment can consist of one or more parcels.
    /// </summary>
    public required IReadOnlyList<Parcel> Parcels { get; init; }

    /// <summary>
    /// Insurance details for the parcel, if applicable. 
    /// </summary>
    public Insurance? Insurance { get; init; }

    /// <summary>
    /// Cash on delivery details for the parcel, if applicable. 
    /// </summary>
    public CashOnDelivery? Cod { get; init; }

    /// <summary>
    /// Service code used by InPost.
    /// </summary>
    public required string Service { get; init; }

    /// <summary>
    /// Custom attributes for the shipment, if applicable.
    /// </summary>
    public CustomAttributes? CustomAttributes { get; init; }

    /// <summary>
    /// Cost center - MPK for the shipment, if applicable. 
    /// </summary>
    public string? Mpk { get; init; }

    /// <summary>
    /// External customer identifier for the shipment, if applicable. 
    /// </summary>
    public string? ExternalCustomerId { get; init; }

    /// <summary>
    /// Reference or note for the shipment, if applicable. The property may be used for internal tracking or identification purposes.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// Is Return shipment. Indicates whether the parcel is being sent as a return to the sender. 
    /// </summary>
    public bool? IsReturn { get; init; }

    /// <summary>
    /// Additional comments, if applicable. 
    /// </summary>
    public string? Comments { get; init; }

    /// <summary>
    /// Additional services for the shipment ex. "email" or "sms", if applicable. This is optional.
    /// </summary>
    public IReadOnlyList<string>? AdditionalServices { get; init; }

    /// <summary>
    /// Setting this to true will make the shipment in draft mode and not be payed immediately.
    /// </summary>
    public bool? OnlyChoiceOfOffer { get; init; }
}