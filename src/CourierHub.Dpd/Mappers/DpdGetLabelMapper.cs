using CourierHub.Abstractions.Models.Requests;
using CourierHub.Core.Utils;
using CourierHub.Dpd.Client.Models.Common;
using CourierHub.Dpd.Client.Models.Requests;
using System;
using System.Collections.Generic;

namespace CourierHub.Dpd.Mappers
{
    /// <summary>
    /// Maps between CourierHub abstraction models and DPD-specific DTOs for get label operation.
    /// </summary>
    internal sealed class DpdGetLabelMapper
    {
        /// <summary>
        /// Maps a standardized label request to DPD-specific format.
        /// </summary>
        /// <param name="source">The source <see cref="GetLabelRequest"/> object.</param>
        /// <returns>The mapped <see cref="DpdGetLabelRequest"/> object.</returns>
        public DpdGetLabelRequest MapToGetLabelRequest(GetLabelRequest source)
            => new()
            {
                LabelSearchParams = new DpdLabelSearchParams
                {
                    Policy = MetadataUtils.GetRequiredMetadataValue<string>(source.Metadata, "Dpd_LabelSearchParams.Policy"),
                    Session = new DpdSession
                    {
                        Type = MetadataUtils.GetRequiredMetadataValue<string>(source.Metadata, "Dpd_Session.Type"),
                        SessionId = source.ParcelId,
                        Packages = []
                    }
                },
                OutputDocFormat = source.Format.ToUpperInvariant(),
                OutputType = source.Type.ToUpperInvariant(),
                Variant = MetadataUtils.GetMetadataValue<string>(source.Metadata, "Dpd_Variant")
            };
    }
}
