using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Internship
{
    public class InternshipUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
      [StringLength(1000, MinimumLength = 10)]

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
        public List<int> SkillIds { get; set; } = new List<int>();

    }
}