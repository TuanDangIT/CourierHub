using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Models.Common;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using CourierHub.Core.UnitConverters;
using CourierHub.InPost.Client.Models.Common;
using CourierHub.InPost.Client.Models.Requests;
using CourierHub.InPost.Client.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourierHub.InPost.Mappers;

/// <summary>
/// Maps between CourierHub abstraction models and InPost-specific DTOs.
/// </summary>
/// <remarks>
/// This mapper translates standardized courier requests/responses into InPost API format,
/// allowing the abstraction layer to remain independent of provider-specific implementations.
/// </remarks>
internal sealed class InPostMapper
{
    /// <summary>
    /// Maps a standardized parcel creation request to InPost-specific format.
    /// </summary>
    /// <param name="source">The source CreateParcelRequest object.</param>
    /// <returns>The mapped InPostCreateParcelRequest object.</returns>
    public InPostCreateParcelRequest MapToCreateParcelRequest(CreateParcelRequest source)
        => new()
        {
            Sender = MapToPeer(source.Sender),
            Receiver = MapToPeer(source.Receiver),
            Parcels = [.. source.Parcels.Select(MapToParcel)],
            Service = source.ServiceCode,
            Insurance = source.Insurance is not null ? MapToInsurance(source.Insurance) : null,
            Cod = source.CashOnDelivery is not null ? MapToCashOnDelivery(source.CashOnDelivery) : null,
            CustomAttributes = new InPostCustomAttributes
            {
                TargetPoint = source.DropoffPointCode,
                SendingMethod = source.SendingMethod,
                DropOffPoint = source.DropoffPointCode
            },
            ExternalCustomerId = source.ExternalCustomerId,
            Mpk = source.CostCenter,
            Reference = source.Reference,
            IsReturn = source.IsReturn,
            Comments = source.Comments,
            AdditionalServices = source.Metadata["InPost_AdditionalServices"] as IEnumerable<string> ?? null
        };

    /// <summary>
    /// Maps an InPost API response to a standardized parcel creation response.
    /// </summary>
    /// <param name="source">The source InPostCreateParcelResponse object.</param>
    /// <returns>The mapped CreateParcelResponse object.</returns>
    public CreateAsyncParcelResponse MapToCreateParcelResponse(InPostCreateParcelResponse source)
        => new()
        {
            ParcelId = source.Id.ToString(),
            Status = source.Status,
            TrackingNumbers = source.Parcels.Select(p => p.TrackingNumber),
            Metadata = new Dictionary<string, object?>
            {
                ["InPost_Href"] = source.Href,
                ["InPost_TrackingNumber"] = source.TrackingNumber,
                ["InPost_Service"] = source.Service,
                ["InPost_Reference"] = source.Reference,
                ["InPost_IsReturn"] = source.IsReturn,
                ["InPost_EndOfWeekCollection"] = source.EndOfWeekCollection,
                ["InPost_ApplicationId"] = source.ApplicationId,
                ["InPost_CreatedById"] = source.CreatedById,
                ["InPost_ExternalCustomerId"] = source.ExternalCustomerId,
                ["InPost_SendingMethod"] = source.SendingMethod,
                ["InPost_Comments"] = source.Comments,
                ["InPost_Mpk"] = source.Mpk,
                ["InPost_AdditionalServices"] = source.AdditionalServices,
                ["InPost_CustomAttributes"] = source.CustomAttributes,
                ["InPost_Cod"] = source.Cod,
                ["InPost_Sender"] = source.Sender,
                ["InPost_Receiver"] = source.Receiver,
                ["InPost_SelectedOffer"] = source.SelectedOffer,
                ["InPost_Offers"] = source.Offers,
                ["InPost_Transactions"] = source.Transactions,
                ["InPost_Parcels"] = source.Parcels,
                ["InPost_Insurance"] = source.Insurance,
                ["InPost_CreatedAt"] = source.CreatedAt,
                ["InPost_UpdatedAt"] = source.UpdatedAt
            }
        };

    /// <summary>
    /// Maps an instance of InPostGetParcelResponse to a GetParcelResponse object.
    /// </summary>
    /// <param name="source">The source InPostGetParcelResponse containing parcel data to be mapped.</param>
    /// <returns>A GetParcelResponse object populated with data from the specified source.</returns>
    public GetParcelCreationStatusResponse MapToGetParcelResponse(InPostGetParcelCreationStatusResponse source)
        => new()
        {
            ParcelId = source.Id.ToString(),
            Status = source.Status,
            TrackingNumbers = source.Parcels.Select(p => p.TrackingNumber),
        };


    /// <summary>
    /// Maps an abstraction Sender/Receiver to InPost Peer format.
    /// </summary>
    /// <param name="source">The source Sender or Receiver object.</param>
    /// <returns>The mapped InPostPeer object.</returns>
    private static InPostPeer MapToPeer(Sender source)
        => new()
        {
            FirstName = source.FirstName,
            LastName = source.LastName,
            Email = source.Email,
            Phone = source.PhoneNumber,
            Address = MapToAddress(source.Address),
            CompanyName = source.CompanyName
        };

    /// <summary>
    /// Maps an abstraction Receiver to InPost Peer format.
    /// </summary>
    /// <param name="source">The source Receiver object.</param>
    /// <returns>The mapped InPostPeer object.</returns>
    private static InPostPeer MapToPeer(Receiver source)
        => new()
        {
            FirstName = source.FirstName,
            LastName = source.LastName,
            Email = source.Email,
            Phone = source.PhoneNumber,
            Address = MapToAddress(source.Address),
            CompanyName = source.CompanyName
        };

    /// <summary>
    /// Maps an abstraction Address to InPost Address format.
    /// </summary>
    /// <param name="source">The source Address object.</param>
    /// <returns>The mapped InPostCreateParcelAddress object.</returns>
    private static InPostAddress MapToAddress(Address source)
        => new()
        {
            Street = source.Street,
            BuildingNumber = source.BuildingNumber,
            ApartmentNumber = source.ApartmentNumber,
            City = source.City,
            PostCode = source.PostalCode,
            CountryCode = source.CountryCode
        };

    /// <summary>
    /// Maps an abstraction Parcel to InPost Parcel format.
    /// </summary>
    /// <param name="source">The source Parcel object.</param>
    /// <returns>The mapped InPostCreateParcelParcel object.</returns>
    private static InPostCreateParcelRequestParcel MapToParcel(Parcel source)
    {
        if (source.Template is not null)
        {
            return new InPostCreateParcelRequestParcel
            {
                Template = source.Template

            };
        } 
        return new InPostCreateParcelRequestParcel
        {
            Dimensions = new InPostDimension
            {
                Length = LengthConverter.Convert(source.Dimension!.Length, source.Dimension.Unit, LengthUnit.Mm),
                Width = LengthConverter.Convert(source.Dimension.Width, source.Dimension.Unit, LengthUnit.Mm),
                Height = LengthConverter.Convert(source.Dimension.Height, source.Dimension.Unit, LengthUnit.Mm),
                Unit = "mm"
            },
            Weight = new InPostWeight
            {
                Amount = WeightConverter.Convert(source.Weight!.Amount, source.Weight.Unit, WeightUnit.Kg),
                Unit = "kg"
            }
        };
    }

    /// <summary>
    /// Maps an abstraction Insurance to InPost Insurance format.
    /// </summary>
    /// <param name="source">The source Insurance object.</param>
    /// <returns>The mapped InPostCreateParcelInsurance object.</returns>
    private static InPostInsurance MapToInsurance(Insurance source)
        => new()
        {
            Amount = source.Amount,
            Currency = source.Currency
        };

    /// <summary>
    /// Maps an abstraction CashOnDelivery to InPost CashOnDelivery format.
    /// </summary>
    /// <param name="source">The source CashOnDelivery object.</param>
    /// <returns>The mapped InPostCreateParcelCashOnDelivery object.</returns>
    private static InPostCashOnDelivery MapToCashOnDelivery(CashOnDelivery source)
        => new()
        {
            Amount = source.Amount,
            Currency = source.Currency
        };
}
