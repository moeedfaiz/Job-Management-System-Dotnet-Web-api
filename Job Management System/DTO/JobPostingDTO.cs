using System.ComponentModel.DataAnnotations;

namespace Job_Management_System.DTO
{
    public class JobPostingDTO
    {
        [Required(ErrorMessage = "Job title is required.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal Salary { get; set; }

        public string Description { get; set; }
    }


}
