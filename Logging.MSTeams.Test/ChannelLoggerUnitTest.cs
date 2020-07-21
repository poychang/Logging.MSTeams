using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.MSTeams.Test
{
    [TestClass]
    public class ChannelLoggerUnitTest
    {
        public string teamsWebhook;
        public LoggerFactory loggerFactory;

        [TestInitialize]
        public void Initialize()
        {
            teamsWebhook = "https://outlook.office.com/webhook/YOUR_TEAMS_WEBHOOK_URL";
            loggerFactory = new LoggerFactory();
        }

        [TestMethod]
        public void TestRun_AddChannelLogger()
        {
            loggerFactory.AddChannelLogger(config =>
            {
                config.LogLevel = LogLevel.Information;
                config.Channel = teamsWebhook;
            });
            loggerFactory.AddChannelLogger(config =>
            {
                config.LogLevel = LogLevel.Debug;
                config.Channel = teamsWebhook;
            });

            var logger = loggerFactory.CreateLogger<ChannelLoggerUnitTest>();

            logger.LogInformation("This is Information.");
            logger.LogDebug("This is Debug.");
            logger.LogWarning("This is Warning.");
        }

        [TestMethod]
        public void TestRun_AddSingleChannelLogger()
        {
            loggerFactory.AddSingleChannelLogger(config =>
            {
                config.Channel = teamsWebhook;
            });

            var logger = loggerFactory.CreateLogger<ChannelLoggerUnitTest>();

            logger.LogInformation("This is Information.");
            logger.LogDebug("This is Debug.");
            logger.LogWarning("This is Warning.");
        }
    }
}
