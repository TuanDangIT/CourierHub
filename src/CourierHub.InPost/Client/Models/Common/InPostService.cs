using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Models.Common
{
    /// <summary>
    /// Represents a service returned by the InPost API.
    /// </summary>
    internal sealed class InPostService
    {
        /// <summary>
        /// The service identifier.
        /// </summary>
        public required string Id { get; init; }

        /// <summary>
        /// The service name.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// The service description.
        /// </summary>
        public required string Description { get; init; }
    }
}
