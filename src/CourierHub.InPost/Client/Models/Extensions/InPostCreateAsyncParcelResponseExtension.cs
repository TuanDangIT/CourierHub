using CourierHub.Abstractions.Interfaces;
using System;
using System.Collections.Generic;

namespace CourierHub.InPost.Client.Models.Extensions
{
    /// <summary>
    /// Strongly typed InPost-specific metadata extension for asynchronous parcel creation responses.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtension : ICourierResponseExtension
    {
        /// <summary>
        /// URI reference to the shipment resource in InPost API.
        /// </summary>
        public string? Href { get; init; }

        /// <summary>
        /// Shipment tracking number.
        /// </summary>
        public string? TrackingNumber { get; init; }

        /// <summary>
        /// InPost service code.
        /// </summary>
        public string? Service { get; init; }

        /// <summary>
        /// User-provided shipment reference.
        /// </summary>
        public string? Reference { get; init; }

        /// <summary>
        /// Indicates whether shipment is a return.
        /// </summary>
        public bool? IsReturn { get; init; }

        /// <summary>
        /// Indicates whether weekend service is enabled.
        /// </summary>
        public bool? EndOfWeekCollection { get; init; }

        /// <summary>
        /// InPost application/organization identifier.
        /// </summary>
        public int? ApplicationId { get; init; }

        /// <summary>
        /// InPost identifier of user who created the shipment.
        /// </summary>
        public int? CreatedById { get; init; }

        /// <summary>
        /// External customer identifier.
        /// </summary>
        public string? ExternalCustomerId { get; init; }

        /// <summary>
        /// Shipment sending method.
        /// </summary>
        public string? SendingMethod { get; init; }

        /// <summary>
        /// Additional shipment comments.
        /// </summary>
        public string? Comments { get; init; }

        /// <summary>
        /// MPK cost center.
        /// </summary>
        public string? Mpk { get; init; }

        /// <summary>
        /// Additional services selected for shipment.
        /// </summary>
        public IEnumerable<string>? AdditionalServices { get; init; }

        /// <summary>
        /// InPost custom attributes.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionCustomAttributes? CustomAttributes { get; init; }

        /// <summary>
        /// Cash on delivery details.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionCod? Cod { get; init; }

        /// <summary>
        /// Sender details.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionSender? Sender { get; init; }

        /// <summary>
        /// Receiver details.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionReceiver? Receiver { get; init; }

        /// <summary>
        /// Selected offer details.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionOffer? SelectedOffer { get; init; }

        /// <summary>
        /// All available offers returned by InPost.
        /// </summary>
        public IEnumerable<InPostCreateAsyncParcelResponseExtensionOffer>? Offers { get; init; }

        /// <summary>
        /// Related shipment transactions.
        /// </summary>
        public IEnumerable<InPostCreateAsyncParcelResponseExtensionTransaction>? Transactions { get; init; }

        /// <summary>
        /// Parcels included in shipment.
        /// </summary>
        public IEnumerable<InPostCreateAsyncParcelResponseExtensionParcel>? Parcels { get; init; }

        /// <summary>
        /// Insurance details.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionInsurance? Insurance { get; init; }

        /// <summary>
        /// Shipment creation timestamp.
        /// </summary>
        public DateTime? CreatedAt { get; init; }

        /// <summary>
        /// Shipment last update timestamp.
        /// </summary>
        public DateTime? UpdatedAt { get; init; }
    }

    /// <summary>
    /// Custom InPost shipment attributes.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionCustomAttributes
    {
        /// <summary>
        /// Target point identifier.
        /// </summary>
        public string? TargetPoint { get; init; }

        /// <summary>
        /// Sending method value.
        /// </summary>
        public string? SendingMethod { get; init; }

        /// <summary>
        /// Drop-off point identifier.
        /// </summary>
        public string? DropOffPoint { get; init; }
    }

    /// <summary>
    /// Cash on delivery details.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionCod
    {
        /// <summary>
        /// COD amount.
        /// </summary>
        public decimal? Amount { get; init; }

        /// <summary>
        /// COD currency.
        /// </summary>
        public string? Currency { get; init; }
    }

    /// <summary>
    /// Insurance details.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionInsurance
    {
        /// <summary>
        /// Insurance amount.
        /// </summary>
        public decimal? Amount { get; init; }

        /// <summary>
        /// Insurance currency.
        /// </summary>
        public string? Currency { get; init; }
    }

    /// <summary>
    /// Sender details extension model.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionSender
    {
        /// <summary>
        /// Sender first name.
        /// </summary>
        public string? FirstName { get; init; }

        /// <summary>
        /// Sender last name.
        /// </summary>
        public string? LastName { get; init; }

        /// <summary>
        /// Sender email.
        /// </summary>
        public string? Email { get; init; }

        /// <summary>
        /// Sender phone.
        /// </summary>
        public string? Phone { get; init; }

        /// <summary>
        /// Sender address.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionAddress? Address { get; init; }

        /// <summary>
        /// Sender company name.
        /// </summary>
        public string? CompanyName { get; init; }
    }

    /// <summary>
    /// Receiver details extension model.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionReceiver
    {
        /// <summary>
        /// Receiver first name.
        /// </summary>
        public string? FirstName { get; init; }

        /// <summary>
        /// Receiver last name.
        /// </summary>
        public string? LastName { get; init; }

        /// <summary>
        /// Receiver email.
        /// </summary>
        public string? Email { get; init; }

        /// <summary>
        /// Receiver phone.
        /// </summary>
        public string? Phone { get; init; }

        /// <summary>
        /// Receiver address.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionAddress? Address { get; init; }

        /// <summary>
        /// Receiver company name.
        /// </summary>
        public string? CompanyName { get; init; }
    }

    /// <summary>
    /// Address extension model.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionAddress
    {
        /// <summary>
        /// Street name.
        /// </summary>
        public string? Street { get; init; }

        /// <summary>
        /// Building number.
        /// </summary>
        public string? BuildingNumber { get; init; }

        /// <summary>
        /// Apartment number.
        /// </summary>
        public string? ApartmentNumber { get; init; }

        /// <summary>
        /// City name.
        /// </summary>
        public string? City { get; init; }

        /// <summary>
        /// Postal code.
        /// </summary>
        public string? PostCode { get; init; }

        /// <summary>
        /// Country code.
        /// </summary>
        public string? CountryCode { get; init; }
    }

    /// <summary>
    /// Offer extension model.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionOffer
    {
        /// <summary>
        /// Offer identifier.
        /// </summary>
        public int? Id { get; init; }

        /// <summary>
        /// Service details.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionService? Service { get; init; }

        /// <summary>
        /// Carrier details.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionCarrier? Carrier { get; init; }

        /// <summary>
        /// Offer additional services.
        /// </summary>
        public IEnumerable<string>? AdditionalServices { get; init; }

        /// <summary>
        /// Offer status.
        /// </summary>
        public string? Status { get; init; }

        /// <summary>
        /// Offer expiration date.
        /// </summary>
        public DateTime? ExpiresAt { get; init; }

        /// <summary>
        /// Offer rate.
        /// </summary>
        public decimal? Rate { get; init; }

        /// <summary>
        /// Offer currency.
        /// </summary>
        public string? Currency { get; init; }

        /// <summary>
        /// Offer unavailability reasons.
        /// </summary>
        public IEnumerable<string>? UnavailabilityReasons { get; init; }
    }

    /// <summary>
    /// Service extension model.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionService
    {
        /// <summary>
        /// Service identifier.
        /// </summary>
        public string? Id { get; init; }

        /// <summary>
        /// Service name.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// Service description.
        /// </summary>
        public string? Description { get; init; }
    }

    /// <summary>
    /// Carrier extension model.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionCarrier
    {
        /// <summary>
        /// Carrier identifier.
        /// </summary>
        public string? Id { get; init; }

        /// <summary>
        /// Carrier name.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// Carrier description.
        /// </summary>
        public string? Description { get; init; }
    }

    /// <summary>
    /// Transaction extension model.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionTransaction
    {
        /// <summary>
        /// Transaction identifier.
        /// </summary>
        public string? Id { get; init; }

        /// <summary>
        /// Transaction status.
        /// </summary>
        public string? Status { get; init; }

        /// <summary>
        /// Transaction creation date.
        /// </summary>
        public DateTime? CreatedAt { get; init; }

        /// <summary>
        /// Transaction update date.
        /// </summary>
        public DateTime? UpdatedAt { get; init; }

        /// <summary>
        /// Related offer identifier.
        /// </summary>
        public int? OfferId { get; init; }
    }

    /// <summary>
    /// Parcel extension model.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionParcel
    {
        /// <summary>
        /// Parcel identifier.
        /// </summary>
        public string? Id { get; init; }

        /// <summary>
        /// Internal identify number.
        /// </summary>
        public string? IdentifyNumber { get; init; }

        /// <summary>
        /// Parcel tracking number.
        /// </summary>
        public string? TrackingNumber { get; init; }

        /// <summary>
        /// Indicates non-standard parcel.
        /// </summary>
        public bool? IsNonStandard { get; init; }

        /// <summary>
        /// Parcel template.
        /// </summary>
        public string? Template { get; init; }

        /// <summary>
        /// Parcel dimensions.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionDimension? Dimensions { get; init; }

        /// <summary>
        /// Parcel weight.
        /// </summary>
        public InPostCreateAsyncParcelResponseExtensionWeight? Weight { get; init; }
    }

    /// <summary>
    /// Dimension extension model.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionDimension
    {
        /// <summary>
        /// Parcel length.
        /// </summary>
        public decimal? Length { get; init; }

        /// <summary>
        /// Parcel width.
        /// </summary>
        public decimal? Width { get; init; }

        /// <summary>
        /// Parcel height.
        /// </summary>
        public decimal? Height { get; init; }

        /// <summary>
        /// Dimension unit.
        /// </summary>
        public string? Unit { get; init; }

        /// <summary>
        /// Indicates non-standard dimensions.
        /// </summary>
        public bool? IsNonStandard { get; init; }
    }

    /// <summary>
    /// Weight extension model.
    /// </summary>
    public record class InPostCreateAsyncParcelResponseExtensionWeight
    {
        /// <summary>
        /// Weight amount.
        /// </summary>
        public decimal? Amount { get; init; }

        /// <summary>
        /// Weight unit.
        /// </summary>
        public string? Unit { get; init; }
    }
}
