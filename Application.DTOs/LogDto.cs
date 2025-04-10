using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LogDto
    {
        public int Id { get; set; }
        public string? MachineName { get; set; }
        public DateTime Logged { get; set; }
        public string? Level { get; set; }
        public string? Message { get; set; }
        public string? Logger { get; set; }
        public string? Properties { get; set; }
        public string? Callsite { get; set; }
        public string? Exception { get; set; }
        public int ApplicationId { get; set; }
    }
}
