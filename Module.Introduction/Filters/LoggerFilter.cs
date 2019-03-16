using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Module.Introduction.Infrastructure;

namespace Module.Introduction.Filters
{
    public class LoggerFilter : IActionFilter
    {
        private readonly ILogger _logger;
        private readonly ApplicationSettings _applicationSettings;

        public LoggerFilter(ILoggerFactory loggerFactory, IOptions<ApplicationSettings> settingsOptions)
        {
            _logger = loggerFactory.CreateLogger<LoggerFilter>();
            _applicationSettings = settingsOptions.Value;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_applicationSettings.AllowLoggingInAction)
            {
                _logger.LogTrace($"{context.ActionDescriptor.DisplayName} is starting");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_applicationSettings.AllowLoggingInAction)
            {
                _logger.LogTrace($"{context.ActionDescriptor.DisplayName} is finished");
            }
        }
    }

}

