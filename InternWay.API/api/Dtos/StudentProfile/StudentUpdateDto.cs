using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.StudentProfile
{
    public class StudentUpdateDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Bio { get; set; } = string.Empty;
        [Required]
        public string LinkedInUrl { get; set; } = string.Empty;
        [Required]
        public string ResumeUrl { get; set; } = string.Empty;
        [Required]
        public List<int> SkillIds { get; set; } = new List<int>();

    }
    
}