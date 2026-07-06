using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Dpd.Client.Models.Common;

/// <summary>
/// DPD specific parameters for searching labels.
/// </summary>
public sealed class LabelSearchParams
{
    /// <summary>
    /// Policy for label generation in case of validation errors. Possible values:
    /// - STOP_ON_FIRST_ERROR - generating of labels is interrupted in the case of the first encountered error. Advised packages up to the first error will be returned.
    /// - IGNORE_ERRORS - no label generation interruption in case of validation error
    /// </summary>
    public required string Policy { get; init; }

    /// <summary>
    /// Session information for the label search request. 
    /// </summary>
    public required Session Session { get; init; }
}
