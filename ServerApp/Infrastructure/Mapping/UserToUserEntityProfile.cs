using AutoMapper;
using WebApplication1.ServerApp.DataAccess.Entities;
using WebApplication1.ServerApp.Сore.Models;

namespace WebApplication1.ServerApp.Infrastructure.Mapping
{
    public class UserToUserEntityProfile : Profile
    {
        UserToUserEntityProfile()
        {
            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => src.PasswordHash));
        }
    }
}
