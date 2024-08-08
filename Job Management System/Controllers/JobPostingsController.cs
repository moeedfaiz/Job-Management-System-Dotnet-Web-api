using Job_Management_System.DTO;
using Job_Management_System.helper;
using Job_Management_System.models;
using Job_Management_System.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingsController : ControllerBase
    {
        private readonly IJobPostingRepository _repository;
        private readonly IErrorLogService _errorLogService;  // Error logging service

        public JobPostingsController(IJobPostingRepository repository, IErrorLogService errorLogService)
        {
            _repository = repository;
            _errorLogService = errorLogService;  // Initialize the error logging service
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPosting>>> GetAll()
        {
            try
            {
                var postings = await _repository.GetAllAsync();
                if (postings.Any())
                    return Ok(postings);
                else
                    return NotFound("No job postings found.");
            }
            catch (Exception ex)
            {
                await _errorLogService.LogErrorAsync(ex, HttpContext.Request.Path);
                return StatusCode(500, "An error occurred while retrieving job postings.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobPosting>> GetById(int id)
        {
            try
            {
                var jobPosting = await _repository.GetByIdAsync(id);
                if (jobPosting == null)
                    return NotFound($"Job posting with ID {id} not found.");
                return Ok(jobPosting);
            }
            catch (Exception ex)
            {
                await _errorLogService.LogErrorAsync(ex, HttpContext.Request.Path);
                return StatusCode(500, "An error occurred while retrieving the job posting.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<JobPosting>> Create([FromBody] JobPostingDTO jobPostingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var jobPosting = new JobPosting
                {
                    JobTitle = jobPostingDto.JobTitle,
                    Description = jobPostingDto.Description,
                    CompanyName = jobPostingDto.CompanyName,
                    Location = jobPostingDto.Location,
                    Salary = jobPostingDto.Salary
                };

                var createdJobPosting = await _repository.AddAsync(jobPosting);
                return CreatedAtAction(nameof(GetById), new { id = createdJobPosting.Id }, createdJobPosting);
            }
            catch (Exception ex)
            {
                await ErrorLogger.LogErrorAsync(ex); // Assuming ErrorLogger is your static class
                return StatusCode(500, "An error occurred while creating the job posting.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JobPostingDTO jobPostingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var jobPostingToUpdate = await _repository.GetByIdAsync(id);
                if (jobPostingToUpdate == null)
                {
                    return NotFound($"No job posting found with ID {id}.");
                }

                jobPostingToUpdate.JobTitle = jobPostingDto.JobTitle;
                jobPostingToUpdate.Description = jobPostingDto.Description;
                jobPostingToUpdate.CompanyName = jobPostingDto.CompanyName;
                jobPostingToUpdate.Location = jobPostingDto.Location;
                jobPostingToUpdate.Salary = jobPostingDto.Salary;

                await _repository.UpdateAsync(jobPostingToUpdate);
                return NoContent(); // Successfully updated
            }
            catch (Exception ex)
            {
                await _errorLogService.LogErrorAsync(ex, HttpContext.Request.Path);
                return StatusCode(500, "An error occurred while updating the job posting.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent(); // Successfully "deleted"
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.LogErrorAsync(ex, HttpContext.Request.Path);
                return NotFound($"No job posting found with ID {id}.");
            }
            catch (Exception ex)
            {
                await _errorLogService.LogErrorAsync(ex, HttpContext.Request.Path);
                return StatusCode(500, "An error occurred while deleting the job posting.");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<JobPosting>>> Search([FromQuery] string? jobTitle, [FromQuery] string? companyName, [FromQuery] string? location)
        {
            try
            {
                var results = await _repository.SearchAsync(jobTitle, companyName, location);
                if (results.Any())
                {
                    return Ok(results);
                }
                return NotFound("No job postings found matching the criteria.");
            }
            catch (Exception ex)
            {
                await _errorLogService.LogErrorAsync(ex, HttpContext.Request.Path);
                return StatusCode(500, "An error occurred while searching for job postings.");
            }
        }
    }
}
