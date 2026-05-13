using CourierHub.Abstractions.Enums;
using CourierHub.Abstractions.Models.Common;
using CourierHub.Abstractions.Models.Requests;
using CourierHub.Abstractions.Models.Responses;
using CourierHub.Core.UnitConverters;
using CourierHub.InPost.Client.Models.Common;
using CourierHub.InPost.Client.Models.Extensions;
using CourierHub.InPost.Client.Models.Requests;
using CourierHub.InPost.Client.Models.Responses;

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
    public InPostCreateAsyncParcelRequest MapToCreateParcelRequest(CreateParcelRequest source)
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
    public CreateAsyncParcelResponse<InPostCreateAsyncParcelResponseExtension> MapToCreateParcelResponse(InPostCreateAsyncParcelResponse source)
        => new()
        {
            ParcelId = source.Id.ToString(),
            Status = source.Status,
            TrackingNumbers = source.Parcels.Select(p => p.TrackingNumber),
            Extension = new InPostCreateAsyncParcelResponseExtension
            {
                Href = source.Href,
                TrackingNumber = source.TrackingNumber,
                Service = source.Service,
                Reference = source.Reference,
                IsReturn = source.IsReturn,
                EndOfWeekCollection = source.EndOfWeekCollection,
                ApplicationId = source.ApplicationId,
                CreatedById = source.CreatedById,
                ExternalCustomerId = source.ExternalCustomerId,
                SendingMethod = source.SendingMethod,
                Comments = source.Comments,
                Mpk = source.Mpk,
                AdditionalServices = source.AdditionalServices,
                CustomAttributes = MapToCustomAttributesExtension(source.CustomAttributes),
                Cod = MapToCodExtension(source.Cod),
                Sender = MapToSenderExtension(source.Sender),
                Receiver = MapToReceiverExtension(source.Receiver),
                SelectedOffer = source.SelectedOffer is not null ? MapToOfferExtension(source.SelectedOffer) : null,
                Offers = source.Offers.Any() ? source.Offers.Select(MapToOfferExtension) : null,
                Transactions = source.Transactions.Any() ? source.Transactions.Select(MapToTransactionExtension) : null,
                Parcels = source.Parcels.Any() ? source.Parcels.Select(MapToParcelExtension) : null,
                Insurance = MapToInsuranceExtension(source.Insurance),
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt
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

    /// <summary>
    /// Maps InPost custom attributes to async parcel response extension custom attributes.
    /// </summary>
    /// <param name="source">The source InPost custom attributes.</param>
    /// <returns>The mapped extension custom attributes, or null when source is null.</returns>
    private static InPostCreateAsyncParcelResponseExtensionCustomAttributes? MapToCustomAttributesExtension(InPostCustomAttributes? source)
        => source is null
            ? null
            : new InPostCreateAsyncParcelResponseExtensionCustomAttributes
            {
                TargetPoint = source.TargetPoint,
                SendingMethod = source.SendingMethod,
                DropOffPoint = source.DropOffPoint
            };

    /// <summary>
    /// Maps InPost cash-on-delivery details to async parcel response extension COD details.
    /// </summary>
    /// <param name="source">The source InPost cash-on-delivery details.</param>
    /// <returns>The mapped extension COD details, or null when source is null.</returns>
    private static InPostCreateAsyncParcelResponseExtensionCod? MapToCodExtension(InPostCashOnDelivery? source)
        => source is null
            ? null
            : new InPostCreateAsyncParcelResponseExtensionCod
            {
                Amount = source.Amount,
                Currency = source.Currency
            };

    /// <summary>
    /// Maps InPost insurance details to async parcel response extension insurance details.
    /// </summary>
    /// <param name="source">The source InPost insurance details.</param>
    /// <returns>The mapped extension insurance details, or null when source is null.</returns>
    private static InPostCreateAsyncParcelResponseExtensionInsurance? MapToInsuranceExtension(InPostInsurance? source)
        => source is null
            ? null
            : new InPostCreateAsyncParcelResponseExtensionInsurance
            {
                Amount = source.Amount,
                Currency = source.Currency
            };

    /// <summary>
    /// Maps InPost peer sender details to async parcel response extension sender details.
    /// </summary>
    /// <param name="source">The source InPost peer.</param>
    /// <returns>The mapped extension sender details.</returns>
    private static InPostCreateAsyncParcelResponseExtensionSender MapToSenderExtension(InPostPeer source)
        => new()
        {
            FirstName = source.FirstName,
            LastName = source.LastName,
            Email = source.Email,
            Phone = source.Phone,
            Address = MapToAddressExtension(source.Address),
            CompanyName = source.CompanyName
        };

    /// <summary>
    /// Maps InPost peer receiver details to async parcel response extension receiver details.
    /// </summary>
    /// <param name="source">The source InPost peer.</param>
    /// <returns>The mapped extension receiver details.</returns>
    private static InPostCreateAsyncParcelResponseExtensionReceiver MapToReceiverExtension(InPostPeer source)
        => new()
        {
            FirstName = source.FirstName,
            LastName = source.LastName,
            Email = source.Email,
            Phone = source.Phone,
            Address = MapToAddressExtension(source.Address),
            CompanyName = source.CompanyName
        };

    /// <summary>
    /// Maps InPost address details to async parcel response extension address details.
    /// </summary>
    /// <param name="source">The source InPost address.</param>
    /// <returns>The mapped extension address details.</returns>
    private static InPostCreateAsyncParcelResponseExtensionAddress MapToAddressExtension(InPostAddress source)
        => new()
        {
            Street = source.Street,
            BuildingNumber = source.BuildingNumber,
            ApartmentNumber = source.ApartmentNumber,
            City = source.City,
            PostCode = source.PostCode,
            CountryCode = source.CountryCode
        };

    /// <summary>
    /// Maps an InPost offer to async parcel response extension offer details.
    /// </summary>
    /// <param name="source">The source InPost offer.</param>
    /// <returns>The mapped extension offer details.</returns>
    private static InPostCreateAsyncParcelResponseExtensionOffer MapToOfferExtension(InPostOffer source)
        => new()
        {
                Id = source.Id,
                Service = new InPostCreateAsyncParcelResponseExtensionService
                {
                    Id = source.Service.Id,
                    Name = source.Service.Name,
                    Description = source.Service.Description
                },
                Carrier = new InPostCreateAsyncParcelResponseExtensionCarrier
                {
                    Id = source.Carrier.Id,
                    Name = source.Carrier.Name,
                    Description = source.Carrier.Description
                },
                AdditionalServices = source.AdditionalServices,
                Status = source.Status,
                ExpiresAt = source.ExpiresAt,
                Rate = source.Rate,
                Currency = source.Currency,
                UnavailabilityReasons = source.UnavailabilityReasons
            };

    /// <summary>
    /// Maps an InPost transaction to async parcel response extension transaction details.
    /// </summary>
    /// <param name="source">The source InPost transaction.</param>
    /// <returns>The mapped extension transaction details.</returns>
    private static InPostCreateAsyncParcelResponseExtensionTransaction MapToTransactionExtension(InPostTransaction source)
        => new()
        {
            Id = source.Id,
            Status = source.Status,
            CreatedAt = source.CreatedAt,
            UpdatedAt = source.UpdatedAt,
            OfferId = source.OfferId
        };

    /// <summary>
    /// Maps an InPost parcel to async parcel response extension parcel details.
    /// </summary>
    /// <param name="source">The source InPost parcel.</param>
    /// <returns>The mapped extension parcel details.</returns>
    private static InPostCreateAsyncParcelResponseExtensionParcel MapToParcelExtension(InPostCreateParcelResponseParcel source)
        => new()
        {
            Id = source.Id,
            IdentifyNumber = source.IdentifyNumber,
            TrackingNumber = source.TrackingNumber,
            IsNonStandard = source.IsNonStandard,
            Template = source.Template,
            Dimensions = source.Dimensions is null
                ? null
                : new InPostCreateAsyncParcelResponseExtensionDimension
                {
                    Length = source.Dimensions.Length,
                    Width = source.Dimensions.Width,
                    Height = source.Dimensions.Height,
                    Unit = source.Dimensions.Unit,
                    IsNonStandard = source.Dimensions.IsNonStandard
                },
            Weight = source.Weight is null
                ? null
                : new InPostCreateAsyncParcelResponseExtensionWeight
                {
                    Amount = source.Weight.Amount,
                    Unit = source.Weight.Unit
                }
        };
}
