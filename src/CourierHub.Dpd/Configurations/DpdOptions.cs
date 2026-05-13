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
        public required string BaseUrl { get; set; } = "https://dpdservices.dpd.com.pl";

        /// <summary>
        /// Login for Basic schema authentication. Required for authentication.
        /// </summary>
        public required string Login { get; set; }

        /// <summary>
        /// Password for Basic schema authentication. Required for authentication.
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// The master financial ID assigned by Dpd. Required for authentication.
        /// </summary>
        public required string MasterFID { get; set; }
    }
}
