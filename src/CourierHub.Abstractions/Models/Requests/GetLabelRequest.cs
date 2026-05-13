using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Requests
{
    /// <summary>
    /// Represents a request to retrieve a shipping label for a previously created parcel. 
    /// </summary>
    public record class GetLabelRequest
    {
        /// <summary>
        /// The unique identifier for the parcel assigned by the courier provider.
        /// </summary>
        /// <remarks>
        /// This ID is used for subsequent API calls to retrieve labels, cancel shipments,
        /// or update parcel information depending on courier provider. Some actions might be not allowed.
        /// </remarks>
        public required string ParcelId { get; init; }

        /// <summary>
        /// The format of the label to be retrieved. The specific values and their meanings are determined by each courier provider. Common formats include "PDF", "ZPL", "PNG", etc. The courier provider may support multiple label formats, and the caller can specify the desired format in this field.
        /// </summary>
        public required string Format { get; init; }

        /// <summary>
        /// The type of label to be retrieved. The specific values and their meanings are determined by each courier provider. Common label types include "A6", "A4", "normal", etc. 
        /// </summary>
        public required string Type { get; init; }

        /// <summary>
        /// Courier-specific metadata and optional parameters.
        /// </summary>
        /// <remarks>
        /// This dictionary serves as an escape hatch for courier-specific fields that don't fit
        /// the standardized model. Each courier provider is responsible for extracting and using
        /// the metadata relevant to it.
        /// 
        /// Key naming convention: "{CourierName}_{PropertyName}" or "{CourierName}_{PropertyName}.{SubPropertyName}" and so on for nested properties.
        /// Example: "InPost_MPK", "Dpd_ServiceType", "Furgonetka_Insurance"
        /// </remarks>
        public Dictionary<string, object?> Metadata { get; init; } = [];
    }
}
