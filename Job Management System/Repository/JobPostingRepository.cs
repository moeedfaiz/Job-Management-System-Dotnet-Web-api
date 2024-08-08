using Job_Management_System.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Job_Management_System.Repository
{
    public class JobPostingRepository : IJobPostingRepository
    {
        private readonly ApplicationDbContext _context;

        public JobPostingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all job postings
        public async Task<IEnumerable<JobPosting>> GetAllAsync()
        {
            return await _context.JobPostings.ToListAsync();
        }

        // Get a single job posting by ID
        public async Task<JobPosting> GetByIdAsync(int id)
        {
            return await _context.JobPostings.FindAsync(id);
        }

        // Add a new job posting
        public async Task<JobPosting> AddAsync(JobPosting jobPosting)
        {
            _context.JobPostings.Add(jobPosting);
            await _context.SaveChangesAsync();
            return jobPosting;
        }

        // Update an existing job posting
        public async Task UpdateAsync(JobPosting jobPosting)
        {
            _context.Entry(jobPosting).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobPostingExists(jobPosting.Id))
                {
                    throw new KeyNotFoundException("No job posting found with this ID.");
                }
                else
                {
                    throw;
                }
            }
        }

        // Delete a job posting
        public async Task DeleteAsync(int id)
        {
            var jobPosting = await _context.JobPostings.FindAsync(id);
            if (jobPosting == null)
            {
                throw new KeyNotFoundException("No job posting found with this ID.");
            }

            jobPosting.IsDeleted = true; // Mark as deleted instead of removing
            await _context.SaveChangesAsync();
        }


        // Search for job postings based on job title, company name, and location
        public async Task<IEnumerable<JobPosting>> SearchAsync(string jobTitle, string companyName, string location)
        {
            IQueryable<JobPosting> query = _context.JobPostings.Where(jp => !jp.IsDeleted);

            if (!string.IsNullOrEmpty(jobTitle))
            {
                query = query.Where(jp => jp.JobTitle.Contains(jobTitle));
            }
            if (!string.IsNullOrEmpty(companyName))
            {
                query = query.Where(jp => jp.CompanyName.Contains(companyName));
            }
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(jp => jp.Location.Contains(location));
            }

            return await query.ToListAsync();
        }




        // Helper method to check if a JobPosting exists
        private bool JobPostingExists(int id)
        {
            return _context.JobPostings.Any(e => e.Id == id);
        }
    }
}
