using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Management_System.errormodel
{
    [Table("ErrorLogs")]
    public class ErrorLog
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Path { get; set; } // Endpoint where error occurred
    }
}
