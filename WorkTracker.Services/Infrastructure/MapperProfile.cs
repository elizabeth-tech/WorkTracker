using AutoMapper;
using WorkTracker.Data.Entities;

namespace WorkTracker.Services.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Core.DTO.Input.ReportDTO, Report>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Date)));
            CreateMap<Report, Core.DTO.Output.ReportDTO>();
            
            CreateMap<Core.DTO.Input.UserDTO, User>();
        }
    }
}
