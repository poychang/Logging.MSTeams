using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Logging.MSTeams
{
    public sealed class ChannelLoggerProvider : ILoggerProvider
    {
        private readonly ChannelLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, ChannelLogger> _loggers;

        public ChannelLoggerProvider(ChannelLoggerConfiguration config)
        {
            _config = config;
            _loggers = new ConcurrentDictionary<string, ChannelLogger>();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new ChannelLogger(name, _config));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
