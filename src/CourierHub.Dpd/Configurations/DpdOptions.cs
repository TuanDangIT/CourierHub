using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace CourierHub.Dpd.Configurations
{

    /// <summary>
    /// Configuration options for Dpd API integration.
    /// </summary>
    public sealed class DpdOptions
    {
        /// <summary>
        /// The base URL for the Dpd API.
        /// Defaults to production URL.
        /// </summary>
        public string BaseUrl { get; set; } = "https://dpdservices.dpd.com.pl";

        /// <summary>
        /// The API key assigned by Dpd. Required for authentication.
        /// </summary>
        public required string ApiKey { get; set; }

        /// <summary>
        /// The organization ID assigned by Dpd. Required for authentication.
        /// </summary>
        public required string OrganizationFID { get; set; }
    }
}
