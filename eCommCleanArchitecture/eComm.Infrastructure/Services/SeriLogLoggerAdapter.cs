using eComm.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace eComm.Infrastructure.Services
{
    public class SeriLogLoggerAdapter<T>(ILogger<T> logger) : IAppLogger<T>
    {
        public void LogError(Exception ex, string message) => logger.LogError(ex, message);

        public void LogInformation(string message) => logger.LogInformation(message);

        public void LogWarning(string message) => logger.LogWarning(message);
       
    }
}
