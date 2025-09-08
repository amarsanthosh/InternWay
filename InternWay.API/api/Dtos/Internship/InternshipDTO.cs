using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Internship
{
public class InternshipDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime PostedOn { get; set; }
    public bool IsActive { get; set; }
    public int RecruiterId { get; set; }
    public List<SkillDto> Skills { get; set; } = new();
}
}