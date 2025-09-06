using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string LinkedInUrl { get; set; } = string.Empty;
        public string ResumeUrl { get; set; } = string.Empty;
        public string? AppUserId { get; set; }         
        public AppUser? AppUser { get; set; }
        public ICollection<StudentSkill> StudentSkills { get; set; } = new List<StudentSkill>();
    }
}