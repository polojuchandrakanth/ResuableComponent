using Microsoft.Extensions.Options;
using NLog;
using NLog.AWS.Logger;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using Microsoft.Extensions.Configuration;

namespace Feature.API.Logger
{
    public class LoggerExtention : ILoggerExtention
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();
        private IConfiguration appconfig;
        public LoggerExtention(IOptions<AppSettings> appSettings, IConfiguration _appconfig)
        {
            appconfig = _appconfig;
            var config = new LoggingConfiguration();
            Logging log = new Logging();
            log.Region = appconfig.GetSection("Logging").GetSection("Region").Value;
            log.AccessId = appconfig.GetSection("Logging").GetSection("AccessId").Value;
            log.AccessKey = appconfig.GetSection("Logging").GetSection("AccessKey").Value;
            log.CloudWatchLogGroup = appconfig.GetSection("Logging").GetSection("LogGroup").Value;
            
            ////Implementing target for AWS

            //var awsTarget = new AWSTarget()
            //{
            //    LogGroup = log.CloudWatchLogGroup,
            //    Region = log.Region,
            //    Credentials = new Amazon.Runtime.BasicAWSCredentials(log.AccessKey,log.AccessId),
            //    Layout = new SimpleLayout
            //    {
            //        Text = "${longdate} ${level:uppercase=true} ${machinename} ${message} ${exception:format=tostring}"
            //    }
            //};
            //config.AddTarget("aws", awsTarget);
            //config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, awsTarget));

            ////Implementing target for file
            var fileTarget = new FileTarget()
            {
                FileName = "${basedir}\\logs\\APIlog.log",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss}: ${message}",
                ArchiveFileName = "${basedir}\\archives\\log.{#####}.txt",
                ArchiveAboveSize = 10240000,
                ArchiveNumbering = ArchiveNumberingMode.Sequence
            };
            config.AddTarget("logfile", fileTarget);
            config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Debug, fileTarget));

            LogManager.Configuration = config;
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogError(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }

        public void LogInformation(string message)
        {
            _logger.Info(message);
        }
    }
    public class AppSettings
    {
        public Logging Logging { get; set; }
    }
    public class Logging
    {
        public string AccessId { get; set; } = "";
        public string AccessKey { get; set; } = "";
        public string CloudWatchLogGroup { get; set; } = "";
        public string Region { get; set; } = "";
    }
}

