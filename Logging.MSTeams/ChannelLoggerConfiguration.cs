using Microsoft.Extensions.Logging;

namespace Logging.MSTeams
{
    public class ChannelLoggerConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
        public string Channel { get; set; }
    }
}
