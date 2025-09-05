using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Application;
using api.Dtos.Internship;
using api.Dtos.Recruiter;
using api.Models;
using AutoMapper;

namespace api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // DTO --> Model  
            CreateMap<ApplicationCreateDto, Application>();
            CreateMap<ApplicationUpdateDto, Application>();

            CreateMap<InternshipCreateDto, Internship>()
            .ForMember(dest => dest.InternshipSkills,
            opt => opt.MapFrom(
                src => src.SkillIds.Select(
                    id => new InternshipSkill { SkillId = id })));

            CreateMap<InternshipUpdateDto, Internship>()
            .ForMember(dest => dest.InternshipSkills,
            opt => opt.MapFrom(
                src => src.SkillIds.Select(
                    id => new InternshipSkill { SkillId = id })));

            CreateMap<RecruiterCreateDto, Recruiter>();
            CreateMap<RecruiterUpdateDto, Recruiter>();

        }
    }
}