using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class StudentSkill
    {
    public int StudentId { get; set; }
    public StudentProfile Student { get; set; } = null!;

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;
        
    }
}