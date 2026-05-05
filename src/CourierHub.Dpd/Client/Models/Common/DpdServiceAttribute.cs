using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Client.Models.Common;

/// <summary>
/// Service attribute for a DPD parcel shipment, consisting of a name and a value. The specific attributes and their meanings depend on the service being used. For example, for the "TIME_FIXED" service, an attribute with the name "DELIVERY_TIME" may be used to specify the exact delivery time (e.g., "12:00").
/// </summary>
internal sealed class DpdServiceAttribute
{
    /// <summary>
    /// The name of the service attribute. The specific names and their meanings depend on the service being used. For example, for the "TIME_FIXED" service, an attribute with the name "DELIVERY_TIME" may be used to specify the exact delivery time (e.g., "12:00").
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The value of the service attribute. The specific values and their meanings depend on the service being used. For example, for the "TIME_FIXED" service with the "DELIVERY_TIME" attribute, the value would specify the exact delivery time (e.g., "12:00").
    /// </summary>
    public required string Value { get; init; }
}
