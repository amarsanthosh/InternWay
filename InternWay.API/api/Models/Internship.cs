using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Internship
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime PostedOn { get; set; }
        public bool IsActive { get; set; }

        public int RecruiterId { get; set; }
        public Recruiter? Recruiter { get; set; }
        public ICollection<InternshipSkill> RequiredSkills { get; set; } = new List<InternshipSkill>();

    }
}