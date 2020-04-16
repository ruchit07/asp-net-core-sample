using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public partial class ErrorLog
    {
        [Key, Required]
        public long ErrorLogId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string Referal { get; set; }
        public string InnerException { get; set; }
        public long? UserId { get; set; }
    }
}
