using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logging.MSTeams
{
    public static class ChannelLoggerExtensions
    {
        /// <summary>
        /// depends on <see cref="LogLevel"/> to send logs to specific Teams webhook channel.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="configure">Setting Teams webhook channel url and <see cref="LogLevel"/></param>
        /// <returns></returns>
        public static ILoggerFactory AddChannelLogger(this ILoggerFactory loggerFactory, Action<ChannelLoggerConfiguration> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            var config = new ChannelLoggerConfiguration();
            configure(config);

            loggerFactory.AddProvider(new ChannelLoggerProvider(config));

            return loggerFactory;
        }

        /// <summary>
        /// Send all king of <see cref="LogLevel"/> logs to single Teams webhook channel.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="configure">Setting Teams webhook channel url. No needs to set <see cref="LogLevel"/>.</param>
        /// <returns></returns>
        public static ILoggerFactory AddSingleChannelLogger(this ILoggerFactory loggerFactory, Action<ChannelLoggerConfiguration> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            var config = new ChannelLoggerConfiguration();
            configure(config);

            GetEnumValues<LogLevel>().ToList().ForEach(level =>
            {
                var levelConfig = new ChannelLoggerConfiguration()
                {
                    LogLevel = level,
                    Channel = config.Channel
                };
                loggerFactory.AddProvider(new ChannelLoggerProvider(levelConfig));
            });

            return loggerFactory;

            IEnumerable<T> GetEnumValues<T>() => Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
