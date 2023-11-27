using AutoMapper;
using TrilhasBrasil.API.Models;
using TrilhasBrasil.API.Models.Dtos;

namespace TrilhasBrasil.API.Profiles
{
    public class AutorMapperProfiles : Profile
    {
        public AutorMapperProfiles()
        {
            CreateMap<Estado, EstadoDto>().ReverseMap();
            CreateMap<Estado, EstadoPostDto>().ReverseMap();
            CreateMap<Estado, EstadoPutDto>().ReverseMap();
            CreateMap<Trilha, TrilhaPostDto>().ReverseMap();
            CreateMap<Trilha, TrilhaPutDto>().ReverseMap();
            CreateMap<Trilha, TrilhaDto>().ReverseMap();
            CreateMap<Dificuldade, DificuldadeDto>().ReverseMap();
        }
    }
}
