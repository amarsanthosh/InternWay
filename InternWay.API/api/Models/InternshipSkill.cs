using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace api.Models
{
    public class InternshipSkill
    {
    public int InternshipId { get; set; }

    [JsonIgnore]
    
    public Internship Internship { get; set; } = null!;

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;
    }
}