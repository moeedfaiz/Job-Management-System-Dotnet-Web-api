using Job_Management_System.models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Job_Management_System.Repository
{
    public interface IJobPostingRepository
    {
        Task<IEnumerable<JobPosting>> GetAllAsync();
        Task<JobPosting> GetByIdAsync(int id);
        Task<JobPosting> AddAsync(JobPosting jobPosting);
        Task UpdateAsync(JobPosting jobPosting);
        Task DeleteAsync(int id);
        Task<IEnumerable<JobPosting>> SearchAsync(string jobTitle, string companyName, string location); // Expanded search functionality
    }
}
