using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using Timesheet.Domain.Models;

namespace Timesheet.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CreateTimeLogRequest, TimeLog>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.LastName))
                .ForMember(dest => dest.Comment, opt => opt.Ignore());
        }
    }
}
