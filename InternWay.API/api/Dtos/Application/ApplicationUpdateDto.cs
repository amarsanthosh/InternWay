using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Application
{
    public class ApplicationUpdateDto
    {
        [Required]
        public int InternshipId { get; set; }
        [Required]
        public int StudentProfileId { get; set; }

        [Required]
        public Status Status { get; set; } = Status.Pending;  // e.g., Pending, Accepted, Rejected

    }
    
}