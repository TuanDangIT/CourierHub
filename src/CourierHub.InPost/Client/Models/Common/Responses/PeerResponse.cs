using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common.Responses;

/// <summary>
/// Represents sender/receiver data in InPost response.
/// </summary>
public sealed class PeerResponse : Peer
{
    /// <summary>
    /// InPost identifier of the peer.
    /// </summary>
    public int Id { get; init; }
}