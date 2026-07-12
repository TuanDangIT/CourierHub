using CourierHub.Core.Base;
using CourierHub.Core.Errors;
using CourierHub.Core.Result;
using CourierHub.Core.Utils;
using CourierHub.InPost.Client.Models.Errors;
using CourierHub.InPost.Client.Models.Requests;
using CourierHub.InPost.Client.Models.Responses;
using CourierHub.InPost.Configurations;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;

namespace CourierHub.InPost.Client;

internal sealed class InPostHttpClient : HttpClientBase
{
    private readonly InPostOptions _inPostOptions;

    public InPostHttpClient(HttpClient httpClient, InPostOptions inPostOptions, ILogger<InPostHttpClient>? logger = default) : base(httpClient, logger)
    {
        ArgumentNullException.ThrowIfNull(inPostOptions);

        _inPostOptions = inPostOptions;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _inPostOptions.ApiKey);
    }

    public Task<Result<CreateParcelResponse>> CreateShipmentAsync(CreateParcelRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var endpoint = $"v1/organizations/{_inPostOptions.OrganizationId}/shipments";

        return PostAsync(
            endpoint,
            request,
            InPostJsonContext.Default.CreateParcelRequest,
            InPostJsonContext.Default.CreateParcelResponse,
            InPostJsonContext.Default.ErrorResponse,
            MapInPostErrors,
            cancellationToken: cancellationToken);
    }

    public Task<Result<CreateBatchParcelsResponse>> CreateBatchParcelsAsync(CreateBatchParcelsRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var endpoint = $"v1/organizations/{_inPostOptions.OrganizationId}/batches";

        return PostAsync(
            endpoint,
            request,
            InPostJsonContext.Default.CreateBatchParcelsRequest,
            InPostJsonContext.Default.CreateBatchParcelsResponse,
            InPostJsonContext.Default.ErrorResponse,
            MapInPostErrors,
            cancellationToken: cancellationToken);
    }

    public Task<Result<PayForParcelResponse>> PayForParcelAsync(PayForParcelRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var endpoint = $"v1/shipments/{Uri.EscapeDataString(request.ShipmentId)}/buy";

        return PostAsync(
            endpoint,
            request,
            InPostJsonContext.Default.PayForParcelRequest,
            InPostJsonContext.Default.PayForParcelResponse,
            InPostJsonContext.Default.ErrorResponse,
            MapInPostErrors,
            cancellationToken: cancellationToken);
    }

    public Task<GetParcelsResponse> GetParcelsAsync(GetParcelsRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = new List<string>();

        QueryStringUtils.Append(query, "id", request.Ids);
        QueryStringUtils.Append(query, "created_at", request.CreatedAt);
        QueryStringUtils.Append(query, "created_at_gteq", request.CreatedAtGteq);
        QueryStringUtils.Append(query, "created_at_lteq", request.CreatedAtLteq);
        QueryStringUtils.Append(query, "tracking_number", request.TrackingNumber);
        QueryStringUtils.Append(query, "tracking_number_cont", request.TrackingNumberCont);
        QueryStringUtils.Append(query, "status", request.Status);
        QueryStringUtils.Append(query, "service", request.Service);
        QueryStringUtils.Append(query, "carrier", request.Carrier);
        QueryStringUtils.Append(query, "insurance_amount_gteq", request.InsuranceAmountGteq);
        QueryStringUtils.Append(query, "insurance_amount_lteq", request.InsuranceAmountLteq);
        QueryStringUtils.Append(query, "cod_amount_gteq", request.CodAmountGteq);
        QueryStringUtils.Append(query, "cod_amount_lteq", request.CodAmountLteq);
        QueryStringUtils.Append(query, "receiver_name", request.ReceiverName);
        QueryStringUtils.Append(query, "receiver_address", request.ReceiverAddress);
        QueryStringUtils.Append(query, "receiver_city", request.ReceiverCity);
        QueryStringUtils.Append(query, "receiver_post_code", request.ReceiverPostCode);
        QueryStringUtils.Append(query, "receiver_country_code", request.ReceiverCountryCode);
        QueryStringUtils.Append(query, "receiver_phone", request.ReceiverPhone);
        QueryStringUtils.Append(query, "receiver_email", request.ReceiverEmail);
        QueryStringUtils.Append(query, "sender_name", request.SenderName);
        QueryStringUtils.Append(query, "sender_address", request.SenderAddress);
        QueryStringUtils.Append(query, "sender_city", request.SenderCity);
        QueryStringUtils.Append(query, "sender_post_code", request.SenderPostCode);
        QueryStringUtils.Append(query, "sender_country_code", request.SenderCountryCode);
        QueryStringUtils.Append(query, "sender_phone", request.SenderPhone);
        QueryStringUtils.Append(query, "sender_email", request.SenderEmail);
        QueryStringUtils.Append(query, "monitoring", request.Monitoring);
        QueryStringUtils.Append(query, "external_customer_id", request.ExternalCustomerId);
        QueryStringUtils.Append(query, "sending_method", request.SendingMethod);
        QueryStringUtils.Append(query, "only_choice_active_offers", request.OnlyChoiceActiveOffers);
        QueryStringUtils.Append(query, "offers_status", request.OffersStatus);
        QueryStringUtils.Append(query, "page", request.Page);
        QueryStringUtils.Append(query, "per_page", request.PerPage);
        QueryStringUtils.Append(query, "sort_by", request.SortBy);
        QueryStringUtils.Append(query, "sort_order", request.SortOrder);

        var queryString = query.Count == 0 ? string.Empty : "?" + string.Join("&", query);
        var endpoint = $"v1/organizations/{_inPostOptions.OrganizationId}/shipments{queryString}";

        return GetAsync(endpoint, InPostJsonContext.Default.GetParcelsResponse, cancellationToken: cancellationToken);
    }

    public Task<GetBatchParcelsResponse> GetBatchParcelsAsync(GetBatchParcelsRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var endpoint = $"v1/batches/{Uri.EscapeDataString(request.Id.ToString())}";

        return GetAsync(endpoint, InPostJsonContext.Default.GetBatchParcelsResponse, cancellationToken: cancellationToken);
    }

    public Task<byte[]> GetLabelAsync(GetLabelRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var endpoint = $"v1/shipments/{Uri.EscapeDataString(request.ShipmentId)}/label?format={Uri.EscapeDataString(request.Format.ToLowerInvariant())}&type={Uri.EscapeDataString(request.Type.ToLowerInvariant())}";

        return GetAsync(endpoint, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Maps the InPost error payload into CourierHub errors.
    /// </summary>
    private static IReadOnlyList<Error> MapInPostErrors(ErrorResponse response)
    {
        var errors = new List<Error>();

        if (response.Status == 400 && string.Equals(response.Error, "validation_failed", StringComparison.OrdinalIgnoreCase) && response.Details is { } details)
        {
            FlattenValidationDetails(details, string.Empty, response.Error, response.Message, errors);
        }

        if (errors.Count == 0)
        {
            errors.Add(new InPostError(response.Error, response.Message));
        }

        return errors;
    }

    /// <summary>
    /// Flattens the nested InPost validation details into individual CourierHub errors.
    /// </summary>
    private static void FlattenValidationDetails(JsonElement element, string path, string code, string message, IList<Error> errors)
    { 
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in element.EnumerateObject())
            {
                var nextPath = string.IsNullOrWhiteSpace(path) ? property.Name : $"{path}.{property.Name}";
                FlattenValidationDetails(property.Value, nextPath, code, message, errors);
            }
            return;
        }

        errors.Add(new InPostError("InPostValidationError", code, element.GetString()));
    }
}
