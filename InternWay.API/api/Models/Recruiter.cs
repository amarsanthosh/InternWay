using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Recruiter
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string AppUserId { get; set; } = string.Empty;
        public AppUser? AppUser { get; set; }
        public string About { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    
    }
}