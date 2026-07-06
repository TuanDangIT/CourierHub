using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common;

/// <summary>
/// Represents a carrier returned by the InPost API.
/// </summary>
public sealed class Carrier
{
    /// <summary>
    /// The carrier identifier.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// The carrier name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The carrier description.
    /// </summary>
    public required string Description { get; init; }
}
