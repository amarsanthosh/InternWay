using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Application;
using api.Dtos.Internship;
using api.Dtos.Recruiter;
using api.Dtos.StudentProfile;
using api.Models;
using AutoMapper;

namespace api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // DTO --> Model  

            //Application
            CreateMap<ApplicationCreateDto, Application>()
                .ForMember(dest => dest.AppliedOn, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<ApplicationUpdateDto, Application>()
                .ForMember(dest => dest.AppliedOn, opt => opt.Ignore());

            //Internship
            CreateMap<InternshipCreateDto, Internship>()
                .ForMember(dest => dest.PostedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.InternshipSkills,
                    opt => opt.MapFrom(
                        src => src.SkillIds.Select(
                            id => new InternshipSkill { SkillId = id })));

            CreateMap<InternshipUpdateDto, Internship>()
                .ForMember(dest => dest.PostedOn, opt => opt.Ignore())
                .ForMember(dest => dest.InternshipSkills,
                    opt => opt.MapFrom(
                        src => src.SkillIds.Select(
                            id => new InternshipSkill { SkillId = id })));


            //Recruiter
            CreateMap<RecruiterCreateDto, Recruiter>();
            CreateMap<RecruiterUpdateDto, Recruiter>();


            //Student
            CreateMap<StudentCreateDto, StudentProfile>()
            .ForMember(dest => dest.StudentSkills, opt => opt.MapFrom(
                src => src.SkillIds.Select(
                    id => new StudentSkill { SkillId = id })));
                    
            CreateMap<StudentUpdateDto, StudentProfile>()
            .ForMember(dest => dest.StudentSkills, opt => opt.MapFrom(
                src => src.SkillIds.Select(
                    id => new StudentSkill { SkillId = id })));        

        }
    }
}