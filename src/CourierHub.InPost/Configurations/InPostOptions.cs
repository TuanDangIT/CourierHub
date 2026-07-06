using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Configurations;

/// <summary>
/// Configuration options for InPost API integration.
/// </summary>
public sealed class InPostOptions
{
    /// <summary>
    /// The organization ID assigned by InPost. Required for authentication.
    /// </summary>
    public required string OrganizationId { get; set; }

    /// <summary>
    /// The API key assigned by InPost. Required for authentication.
    /// </summary>
    public required string ApiKey { get; set; }

    /// <summary>
    /// The base URL for the InPost API.
    /// </summary>
    public required string BaseUrl { get; set; }
}
