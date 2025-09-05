using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Application;
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
        }
    }
}