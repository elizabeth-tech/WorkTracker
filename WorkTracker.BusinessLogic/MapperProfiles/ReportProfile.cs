using AutoMapper;
using WorkTracker.Contracts.Models.Report;
using WorkTracker.DataAccess.Entities;

namespace WorkTracker.BusinessLogic.MapperProfiles
{
    /// <summary>
	/// Профиль маппинга отчетов
	/// </summary>
	public class ReportProfile : Profile
    {
        /// <inheritdoc />
        public ReportProfile()
        {
            CreateMap<Report, ReportOutDto>().ReverseMap();
            CreateMap<ReportInputDto, Report>()
                .ForMember(x => x.Date, opt => opt.MapFrom(m => DateOnly.FromDateTime(m.Date)));
        }
    }
}
