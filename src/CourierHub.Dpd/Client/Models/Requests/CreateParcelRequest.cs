using CourierHub.Dpd.Client.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Client.Models.Requests;

/// <summary>
/// Create parcel request model specific to DPD API. This class encapsulates all the necessary information required by DPD to process a parcel creation request.
/// </summary>
public sealed class CreateParcelRequest
{
    /// <summary>
    /// Method of generating packages and parcels. Possible values:
    /// - STOP_ON_FIRST_ERROR - generating of parcels is interrupted in the case of the first encountered error. Packages and shipments created up to the first error will be returned.
    /// - IGNORE_ERRORS - no parcel generation interruption in case of validation error.
    /// - ALL_OR_NOTHING - all packages will be returned or none if there is an error.
    /// </summary>
    public required string GenerationPolicy { get; init; } = "STOP_ON_FIRST_ERROR";

    /// <summary>
    /// Packages to be created in DPD system. Each package includes sender and receiver information, as well as the list of parcels to be shipped.
    /// </summary>
    public required IEnumerable<CreateParcelRequestPackage> Packages { get; init; }
}

/// <summary>
/// DPD specific representation of a package, used in DPD API requests and responses.
/// </summary>
public sealed class CreateParcelRequestPackage
{
    /// <summary>
    /// Sender information.
    /// </summary>
    public required SenderOrReceiver Sender { get; init; }

    /// <summary>
    /// Receiver information. This property is required for standard shipments where the sender provides the receiver's details. However, for PUDO (Pick-Up Drop-Off) shipments, this property may be null, and the receiver information will instead be provided in the PudoReceiver property of the service attributes.
    /// </summary>
    public SenderOrReceiver? Receiver { get; init; }

    /// <summary>
    /// Receiver information. This property is specifically used for PUDO (Pick-Up Drop-Off) shipments.
    /// </summary>
    public PudoReceiver? PudoReceiver { get; init; }

    /// <summary>
    /// Parcels to be shipped. Each parcel includes its dimensions and weight.
    /// </summary>
    public required IEnumerable<CreateParcelRequestParcel> Parcels { get; init; }

    /// <summary>
    /// Reference or note for the shipment, if applicable. The property may be used for internal tracking or identification purposes. This value must be unique if used.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    /// Payer FID (Financial Identifier) for the shipment. 
    /// </summary>
    public required string PayerFID { get; init; }

    /// <summary>
    /// Customer-specific info for the shipment, if applicable. This is an optional field that can be used to provide any additional information or notes related to the shipment. 
    /// </summary>
    /// <remarks>This field is shown in the first row of the label in customer-specific info section.</remarks>
    public string? Ref1 { get; init; }

    /// <summary>
    /// Customer-specific info for the shipment, if applicable. This is an optional field that can be used to provide any additional information or notes related to the shipment. 
    /// </summary>
    /// <remarks>This field is shown in the second row of the label in customer-specific info section.</remarks>
    public string? Ref2 { get; init; }

    /// <summary>
    /// Customer-specific info for the shipment, if applicable. This is an optional field that can be used to provide any additional information or notes related to the shipment. 
    /// </summary>
    /// <remarks>This field is shown in the third row of the label in customer-specific info section.</remarks>
    public string? Ref3 { get; init; }

    /// <summary>
    /// List of services to be applied to the shipment. Example: "TIME_FIXED" with delivery time at exact time 12:00.
    /// </summary>
    public IEnumerable<Service>? Service { get; init; }
}

/// <summary>
/// DPD specific representation of a parcel, used in DPD API requests and responses.
/// </summary>
public sealed class CreateParcelRequestParcel
{
    /// <summary>
    /// Weight of the parcel in kilograms (kg). DPD API expects weight to be provided in kg, even if the original request uses a different unit. The abstraction layer is responsible for converting weight to kg before mapping to DpdParcel.
    /// </summary>
    public required decimal Weight { get; init; }

    /// <summary>
    /// Specific weight for ADR (dangerous goods) shipments. This is an optional field that can be used to provide the weight of the parcel specifically for ADR shipments. 
    /// </summary>
    /// <remarks>Minimum value is 0 kg and maximum is weight of the parcel.</remarks>
    public decimal? WeightAdr { get; init; }

    /// <summary>
    /// Length of the parcel in centimeters (cm). DPD API expects dimensions to be provided in cm, even if the original request uses a different unit. The abstraction layer is responsible for converting dimensions to cm before mapping to DpdParcel.
    /// </summary>
    public required decimal SizeX { get; init; }

    /// <summary>
    /// Width of the parcel in centimeters (cm). DPD API expects dimensions to be provided in cm, even if the original request uses a different unit. The abstraction layer is responsible for converting dimensions to cm before mapping to DpdParcel.
    /// </summary>
    public required decimal SizeY { get; init; }

    /// <summary>
    /// Depth of the parcel in centimeters (cm). DPD API expects dimensions to be provided in cm, even if the original request uses a different unit. The abstraction layer is responsible for converting dimensions to cm before mapping to DpdParcel.
    /// </summary>
    public required decimal SizeZ { get; init; }

    /// <summary>
    /// Content of the parcel. This is an optional field that can be used to provide a description of the contents of the parcel. 
    /// </summary>
    /// <remarks>This content string might be displayed on the final label for DPD.</remarks>
    public string? Content { get; init; }

    /// <summary>
    /// Additional customer-specific data for the parcel, if applicable. This is an optional field that can be used to provide any extra information or notes related to the parcel.
    /// </summary>
    public string? CustomerData1 { get; init; }

    /// <summary>
    /// Additional customer-specific data for the parcel, if applicable. This is an optional field that can be used to provide any extra information or notes related to the parcel.
    /// </summary>
    public string? CustomerData2 { get; init; }

    /// <summary>
    /// Additional customer-specific data for the parcel, if applicable. This is an optional field that can be used to provide any extra information or notes related to the parcel.
    /// </summary>
    public string? CustomerData3 { get; init; }

    /// <summary>
    /// Reference for the parcel ex. external database guid.
    /// </summary>
    public string? Reference { get; init; }
}
