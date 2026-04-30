using CourierHub.InPost.Client.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Responses;

internal sealed class InPostGetParcelCreationStatusResponse
{
    /// <summary>
    /// The Identifier of the shipment in the InPost system. 
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// The current status of the shipment in the InPost system.
    /// </summary>
    public required string Status { get; init; }

}
