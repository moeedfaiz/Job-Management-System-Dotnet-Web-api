namespace Job_Management_System.Repository
{
    public interface IErrorLogService
    {
        Task LogErrorAsync(Exception ex, string path);
    }
}
