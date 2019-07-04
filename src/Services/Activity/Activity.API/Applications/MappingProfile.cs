using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.API.Applications.Commands;
using Together.Activity.API.Models;

namespace Together.Activity.API.Applications
{
    public class MappingProfile
        : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateActivityViewModel, CreateActivityCommand>()
                .ReverseMap();
        }
    }
}
