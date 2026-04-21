using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents a shipping service offered by InPost.
/// </summary>
internal sealed class InPostService
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}
