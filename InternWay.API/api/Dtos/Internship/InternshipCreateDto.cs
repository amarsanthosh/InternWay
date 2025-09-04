using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Internship
{
    public class InternshipCreateDto
    {

        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        [Range(10, 1000)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public DateTime PostedOn { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int RecruiterId { get; set; }
        [Required]
        public ICollection<InternshipSkill> InternshipSkills { get; set; } = new List<InternshipSkill>();
    }
            
}