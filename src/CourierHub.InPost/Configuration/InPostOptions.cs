using System;
using System.Collections.Generic;
using System.Text;
using CourierHub.Core.Configuration;

namespace CourierHub.InPost.Configuration;

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
    /// The base URL for the InPost API (e.g., "https://api-shipx-pl.inpost.tech").
    /// Defaults to production URL.
    /// </summary>
    public string BaseUrl { get; set; } = "https://api-shipx-pl.inpost.tech";
}
