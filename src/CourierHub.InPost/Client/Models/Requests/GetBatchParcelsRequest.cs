using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Requests;

/// <summary>
/// InPost batch lookup request model.
/// </summary>
public sealed class GetBatchParcelsRequest
{
    /// <summary>
    /// Batch identifier.
    /// </summary>
    public required int Id { get; init; }
}
