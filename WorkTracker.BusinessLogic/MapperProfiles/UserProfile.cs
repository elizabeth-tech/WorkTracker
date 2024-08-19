using AutoMapper;
using WorkTracker.Contracts.Models.User;
using WorkTracker.DataAccess.Entities;

namespace WorkTracker.BusinessLogic.MapperProfiles
{
    /// <summary>
    /// Профиль маппинга пользователей
    /// </summary>
    public class UserProfile : Profile
    {
        /// <inheritdoc />
        public UserProfile()
        {
            CreateMap<User, UserOutDto>().ReverseMap();
            CreateMap<User, UserInputDto>().ReverseMap();
        }
    }
}
