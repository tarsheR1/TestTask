using AutoMapper;
using WebApplication1.ServerApp.Сore.Models;
using WebApplication1.ServerApp.DataAccess.Entities;
namespace WebApplication1.ServerApp.Infrastructure.Mapping
{
    public class EventToEventEntity : Profile
    {
        public EventToEventEntity() 
        { 
        CreateMap<Event, EventEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.EventDateTime, opt => opt.MapFrom(src => src.EventDateTime))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.MaxParticipants, opt => opt.MapFrom(src => src.MaxParticipants))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.UserSubscribed, opt => opt.MapFrom(src => src.UserSubscribed));
        }
    }
}
