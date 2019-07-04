using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Infrastructure.Dtos;
using WebMVC.ViewModels.Activity;

namespace WebMVC.Infrastructure.Mapper
{
    public class MappingProfile
        : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActivityCreateViewModel, ActivityCreateDto>();

        }
    }
}
