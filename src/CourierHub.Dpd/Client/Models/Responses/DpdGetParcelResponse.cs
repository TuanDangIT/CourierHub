using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Client.Models.Responses
{
    /// <summary>
    /// Represents the response from a successful parcel retrieval request in Dpd API.
    /// </summary>
    internal sealed class DpdGetParcelResponse
    {
        /// <summary>
        /// Label data in Base64 format.
        /// </summary>
        public required string DocumentData { get; set; }
    }
}
