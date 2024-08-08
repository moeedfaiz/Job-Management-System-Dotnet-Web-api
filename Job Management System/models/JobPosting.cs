using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Management_System.models
{
    [Table("JobPostings")]
    public class JobPosting
    {
        [Key]
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public bool IsDeleted { get; set; }
    }
}
