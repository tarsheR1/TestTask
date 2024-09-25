using AutoMapper;

namespace WebApplication1.ServerApp.Infrastructure.Mapping
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
