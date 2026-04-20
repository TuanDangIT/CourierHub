using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Models.Responses
{
    /// <summary>
    /// Represents the response from a label retrieval request.
    /// </summary>
    public record GetLabelResponse
    {
        /// <summary>
        /// The label content as a byte array.
        /// </summary>
        /// <remarks>
        /// This is the raw label data that can be saved to a file, uploaded to cloud storage (S3),
        /// or sent directly to a printer. The format is determined by the ContentType property.
        /// </remarks>
        public required byte[] LabelContent { get; init; }

        /// <summary>
        /// The MIME type of the label content (e.g., "application/pdf", "image/png", "text/plain").
        /// </summary>
        /// <remarks>
        /// This indicates how the label data should be interpreted and processed.
        /// Common types:
        /// - "application/pdf": PDF document
        /// - "image/png": PNG image
        /// - "image/jpeg": JPEG image
        /// - "text/plain": ZPL or other text-based format
        /// </remarks>
        public required string ContentType { get; init; }

        /// <summary>
        /// Suggested file extension for saving the label (e.g., "pdf", "png", "zpl").
        /// </summary>
        /// <remarks>
        /// This is optional but helpful for determining the appropriate file extension
        /// when saving the label to disk.
        /// </remarks>
        public string? FileExtension { get; init; }

        /// <summary>
        /// The tracking number associated with this label.
        /// </summary>
        public required string TrackingNumber { get; init; }

        /// <summary>
        /// Additional metadata from the courier provider.
        /// </summary>
        /// <remarks>
        /// May include information like label format version, print settings, or other
        /// courier-specific details.
        /// </remarks>
        public Dictionary<string, object> Metadata { get; init; } = [];
    }
}
