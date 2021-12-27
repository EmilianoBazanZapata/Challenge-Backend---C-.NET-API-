using API.Data;
using API.Models;
using AutoMapper;
namespace API.Entities
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Character, CharacterDTO>().ReverseMap();
            CreateMap<Character, CreateCharacterDTO>().ReverseMap();
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<Movie, CreateMovieDTO>().ReverseMap();
            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}
