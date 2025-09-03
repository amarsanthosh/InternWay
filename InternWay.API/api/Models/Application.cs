using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Application
    {
        public int Id { get; set; }
        public int InternshipId { get; set; }
        public Internship? Internship { get; set; }
        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = null!;

        public DateTime AppliedOn { get; set; }
        public Status Status { get; set; }  // e.g., Pending, Accepted, Rejected
    }
}