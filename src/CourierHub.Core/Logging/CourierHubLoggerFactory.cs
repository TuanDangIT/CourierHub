using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Core.Logging
{
    /// <summary>
    /// Provides a factory for creating configured loggers for CourierHub components.
    /// </summary>
    public static class CourierHubLoggerFactory
    {
        /// <summary>
        /// Creates a configured ILoggerFactory.
        /// </summary>
        /// <param name="configure">Optional configuration action for logger options.</param>
        /// <returns>A configured ILoggerFactory instance.</returns>
        public static ILoggerFactory CreateLoggerFactory(Action<LoggerOptions>? configure = null)
        {
            var options = new LoggerOptions();
            configure?.Invoke(options);

            return LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(options.MinimumLevel);

                builder.AddFilter("CourierHub", options.MinimumLevel);

                foreach (var (category, level) in options.Filters)
                    builder.AddFilter(category, level);

                if (options.EnableConsole)
                    builder.AddConsole();
            });
        }

        /// <summary>
        /// Convenience: creates a factory and immediately returns a typed logger.
        /// </summary>
        /// <typeparam name="T">The type for which to create the logger.</typeparam>
        /// <param name="configure">Optional configuration action for logger options.</param>
        /// <returns>A configured ILogger instance for the specified type.</returns>
        public static ILogger<T> CreateLogger<T>(Action<LoggerOptions>? configure = null)
        {
            var factory = CreateLoggerFactory(configure);
            return factory.CreateLogger<T>();
        }
    }
}
