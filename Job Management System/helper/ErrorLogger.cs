using Job_Management_System.Repository;

namespace Job_Management_System.helper
{
    public static class ErrorLogger
    {
        public static async Task LogErrorAsync(Exception ex)
        {
            var httpContextAccessor = ServiceLocator.ServiceProvider.GetService<IHttpContextAccessor>();
            var errorLogService = httpContextAccessor.HttpContext.RequestServices.GetService<IErrorLogService>();
            if (errorLogService != null)
            {
                var path = httpContextAccessor.HttpContext.Request.Path;
                await errorLogService.LogErrorAsync(ex, path);
            }
            else
            {
                Console.Error.WriteLine("Error Log Service is not available.");
            }
        }
    }
    public static class ServiceLocator
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static T GetService<T>()
        {
            return (T)ServiceProvider.GetService(typeof(T));
        }
    }

}
