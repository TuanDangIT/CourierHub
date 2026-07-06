using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Core.Logging
{
    /// <summary>
    /// Represents configuration options for the CourierHub logging system.
    /// </summary>
    public sealed class LoggerOptions
    {
        /// <summary>
        /// The minimum log level for all loggers created by the factory.
        /// </summary>
        public LogLevel MinimumLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// The dictionary of category-specific log level filters.
        /// </summary>
        public Dictionary<string, LogLevel> Filters { get; } = [];

        /// <summary>
        /// Indicates whether console logging is enabled.
        /// </summary>
        public bool EnableConsole { get; set; } = true;

        /// <summary>
        /// Adds a filter for a specific category and log level.
        /// </summary>
        /// <param name="category">The category for which to apply the filter.</param>
        /// <param name="level">The log level to apply for the specified category.</param>
        /// <returns>The current LoggerOptions instance for chaining.</returns>
        public LoggerOptions AddFilter(string category, LogLevel level)
        {
            Filters[category] = level;
            return this;
        }
    }
}
