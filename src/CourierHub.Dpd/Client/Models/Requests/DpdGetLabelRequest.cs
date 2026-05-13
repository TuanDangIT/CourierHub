using CourierHub.Dpd.Client.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Client.Models.Requests
{
    /// <summary>
    /// Represents a request to retrieve a shipping label from DPD for a previously created parcel. 
    /// </summary>
    internal class DpdGetLabelRequest
    {
        /// <summary>
        /// Label search params used to find the label for a given parcel.
        /// </summary>
        public required DpdLabelSearchParams LabelSearchParams { get; set; }

        /// <summary>
        /// Label format. Allowed values:
        /// - PDF - pdf
        /// - EPL - epl
        /// - ZPL - zpl
        /// - XML - xml
        /// </summary>
        public required string OutputDocFormat { get; set; }

        /// <summary>
        /// Type of label. Allowed values:
        /// - BIC3 
        /// - EXTENDED
        /// - RETURN - allowed only for international returns
        /// </summary>
        public required string OutputType { get; set; }

        /// <summary>
        /// Label variant. Allowed values:
        /// - STANDARD - standard
        /// - APOLLO - apollo
        /// - RUCH - ruch
        /// </summary>
        public string? Variant { get; set; }
    }
}
