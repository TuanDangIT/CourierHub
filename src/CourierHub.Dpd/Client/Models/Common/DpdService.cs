using CourierHub.Dpd.Client.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Client.Models.Common;

/// <summary>
/// Represents a service to be applied to a DPD parcel shipment, including the service code and any associate attributes.
/// </summary>
internal sealed class DpdService
{
    /// <summary>
    /// The code or identifier of the service to be applied to the shipment (e.g., "TIME_FIXED"). The specific values and their meanings are determined by each courier provider. This field is required if any services are being applied to the shipment.
    /// </summary>
    public required string Code { get; init; }
    /// <summary>
    /// List of attributes for the service. Each attribute consists of a name and a value, which provide additional details about how the service should be applied to the shipment. The specific attributes and their meanings depend on the service being used. For example, for the "TIME_FIXED" service, an attribute with the name "DELIVERY_TIME" may be used to specify the exact delivery time (e.g., "12:00").
    /// </summary>
    public IEnumerable<DpdServiceAttribute>? Attributes { get; init; }
}
