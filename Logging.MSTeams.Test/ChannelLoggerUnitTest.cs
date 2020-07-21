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
            teamsWebhook = "https://outlook.office.com/webhook/70ed5294-ddfb-4eab-9be5-6a8c430661e2@f43a2d89-bcd5-425f-981c-f7231bcd4467/IncomingWebhook/7f6df48fc07849fc917f557f719b569d/3098eded-4c4c-498b-8619-90a141fc88c7";
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
