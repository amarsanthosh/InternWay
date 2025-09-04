using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Recruiter
{
    public class RecruiterUpdateDto
    {
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        public string About { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;
    }
}