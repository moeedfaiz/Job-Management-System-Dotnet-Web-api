using Job_Management_System.errormodel;  // Ensure correct namespace for ErrorLog model
using Job_Management_System.models;      // Ensure correct namespace for DbContext
using Microsoft.AspNetCore.Http;         // For accessing HttpContext
using Microsoft.Extensions.Logging;      // For internal logging within the service
using System;
using System.Threading.Tasks;

namespace Job_Management_System.Repository
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ErrorLogService> _logger; // Logger for the error logging service itself

        public ErrorLogService(ApplicationDbContext dbContext, ILogger<ErrorLogService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task LogErrorAsync(Exception ex, string path)
        {
            try
            {
                var errorLog = new ErrorLog
                {
                    Message = "this is error",
                    StackTrace = "this is error",
                    Path = path,
                    Timestamp = DateTime.UtcNow
                };

                _dbContext.ErrorLogs.Add(errorLog);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception loggingException)
            {
                // Log the error logging failure somewhere it can be monitored
                _logger.LogError(loggingException, "Failed to log error.");
            }
        }
    }
}
