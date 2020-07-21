using MessageCardModel;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Logging.MSTeams
{
    public class ChannelLogger : ILogger
    {
        private readonly string _name;
        private readonly ChannelLoggerConfiguration _config;

        public ChannelLogger(string name, ChannelLoggerConfiguration config)
        {
            _name = name;
            _config = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            if (string.IsNullOrEmpty(_config.Channel)) return;
            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(_config.Channel)
                };
                httpClient.DefaultRequestHeaders.Add("cache-control", "no-cache");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var message = new MessageCard
                {
                    ThemeColor = "0076D7",
                    Summary = $"{_name} Logging",
                    Sections = new[]
                    {
                        new Section
                        {
                            Facts = new[]
                            {
                                new Fact{Name = "Name", Value = $"{_name}" },
                                new Fact{Name = "Event ID", Value = $"{eventId.Id}" },
                                new Fact{Name = "Log Level", Value = $"{logLevel}" },
                                new Fact{Name = "Message", Value = $"{formatter(state, exception)}" }
                            }
                        }
                    }
                };
                var content = new StringContent(message.ToJson(), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(string.Empty, content).GetAwaiter().GetResult();

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
