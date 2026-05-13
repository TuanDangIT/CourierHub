using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Models.Common;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using CourierHub.Core.UnitConverters;
using CourierHub.Core.Utils;
using CourierHub.Dpd.Client.Models.Common;
using CourierHub.Dpd.Client.Models.Requests;
using CourierHub.Dpd.Client.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CourierHub.Dpd.Mappers;

/// <summary>
/// Maps between CourierHub abstraction models and DPD-specific DTOs for create parcel operation.
/// </summary>
internal sealed class DpdCreateParcelMapper
{
    /// <summary>
    /// Maps a standardized parcel creation request to DPD-specific format.
    /// </summary>
    /// <param name="source">The source <see cref="CreateParcelRequest"/> object.</param>
    /// <returns>The mapped <see cref="DpdCreateParcelRequest"/> object.</returns>
    public DpdCreateParcelRequest MapToCreateParcelRequest(CreateParcelRequest source)
        => new()
        {
            GenerationPolicy = MetadataUtils.GetMetadataValue<string>(source.Metadata, "Dpd_GenerationPolicy") ?? "STOP_ON_FIRST_ERROR",
            Packages = [MapToPackage(source)]
        };

    /// <summary>
    /// Maps a DPD API response to a standardized parcel creation response.
    /// </summary>
    /// <param name="source">The source <see cref="DpdCreateParcelResponse"/> object.</param>
    /// <returns>The mapped <see cref="CreateParcelResponse"/> object.</returns>
    public CreateParcelResponse MapToCreateParcelResponse(DpdCreateParcelResponse source)
        => new()
        {
            ParcelId = source.SessionId.ToString(),
            TrackingNumbers = source.Packages.SelectMany(package => package.Parcels).Select(parcel => parcel.Waybill),
            Metadata = new Dictionary<string, string?>
            {
                ["Dpd_Status"] = source.Status,
                ["Dpd_SessionId"] = source.SessionId.ToString(),
                ["Dpd_BeginTime"] = source.BeginTime.ToString("O"),
                ["Dpd_EndTime"] = source.EndTime.ToString("O"),
                ["Dpd_TraceId"] = source.TraceId,
                ["Dpd_Packages"] = JsonSerializer.Serialize(source.Packages)
            }
        };

    /// <summary>
    /// Maps a standardized create parcel request to a single DPD package.
    /// </summary>
    private static DpdCreateParcelRequestPackage MapToPackage(CreateParcelRequest source)
    {
        var pudoReceiver = MetadataUtils.GetMetadataValue<DpdPudoReceiver>(source.Metadata, "Dpd_PudoReceiver");

        return new DpdCreateParcelRequestPackage
        {
            Sender = MapToSenderOrReceiver(source.Sender),
            Receiver = pudoReceiver is null ? MapToSenderOrReceiver(source.Receiver) : null,
            PudoReceiver = pudoReceiver,
            Parcels = [.. source.Parcels.Select(parcel => MapToParcel(parcel, source.Metadata, source.Reference))],
            Reference = source.Reference,
            PayerFID = MetadataUtils.GetRequiredMetadataValue<string>(source.Metadata, "Dpd_PayerFID"),
            Ref1 = MetadataUtils.GetMetadataValue<string>(source.Metadata, "Dpd_Ref1"),
            Ref2 = MetadataUtils.GetMetadataValue<string>(source.Metadata, "Dpd_Ref2"),
            Ref3 = MetadataUtils.GetMetadataValue<string>(source.Metadata, "Dpd_Ref3"),
            Service = MapToServices(source)
        };
    }

    /// <summary>
    /// Maps a standardized sender to DPD sender format.
    /// </summary>
    private static DpdSenderOrReceiver MapToSenderOrReceiver(Sender source)
        => new()
        {
            Name = source.FirstName + " " + source.LastName,
            Email = source.Email,
            Address = AddressFormattingUtils.GetStreetAddress(source.Address),
            PostalCode = AddressFormattingUtils.NormalizePostalCode(source.Address.PostalCode),
            City = source.Address.City,
            CountryCode = source.Address.CountryCode,
            Phone = source.PhoneNumber,
            CompanyName = source.CompanyName
        };

    /// <summary>
    /// Maps a standardized receiver to DPD sender format.
    /// </summary>
    private static DpdSenderOrReceiver MapToSenderOrReceiver(Receiver source)
        => new()
        {
            Name = source.FirstName + " " + source.LastName,
            Email = source.Email,
            Address = AddressFormattingUtils.GetStreetAddress(source.Address),
            PostalCode = AddressFormattingUtils.NormalizePostalCode(source.Address.PostalCode),
            City = source.Address.City,
            CountryCode = source.Address.CountryCode,
            Phone = source.PhoneNumber,
            CompanyName = source.CompanyName
        };

    /// <summary>
    /// Maps a standardized parcel to DPD parcel format.
    /// </summary>
    private static DpdCreateParcelRequestParcel MapToParcel(Parcel source, IReadOnlyDictionary<string, object> metadata, string? reference)
        => new()
        {
            Weight = WeightConverter.Convert(source.Weight!.Amount, source.Weight.Unit, WeightUnit.Kg),
            WeightAdr = MetadataUtils.GetMetadataValue<decimal?>(metadata, "Dpd_WeightAdr"),
            SizeX = LengthConverter.Convert(source.Dimension!.Length, source.Dimension.Unit, LengthUnit.Cm),
            SizeY = LengthConverter.Convert(source.Dimension.Width, source.Dimension.Unit, LengthUnit.Cm),
            SizeZ = LengthConverter.Convert(source.Dimension.Height, source.Dimension.Unit, LengthUnit.Cm),
            Content = MetadataUtils.GetMetadataValue<string>(metadata, "Dpd_Content"),
            CustomerData1 = MetadataUtils.GetMetadataValue<string>(metadata, "Dpd_CustomerData1"),
            CustomerData2 = MetadataUtils.GetMetadataValue<string>(metadata, "Dpd_CustomerData2"),
            CustomerData3 = MetadataUtils.GetMetadataValue<string>(metadata, "Dpd_CustomerData3"),
            Reference = reference
        };

    /// <summary>
    /// Maps abstraction service information to DPD services.
    /// </summary>
    private static IEnumerable<DpdService> MapToServices(CreateParcelRequest source)
    {
        var attributes = MetadataUtils.GetMetadataValue<IEnumerable<DpdServiceAttribute>>(source.Metadata, "Dpd_ServiceAttributes");

        return [
            new DpdService
                {
                    Code = source.ServiceCode,
                    Attributes = attributes
                }
        ];
    }
}