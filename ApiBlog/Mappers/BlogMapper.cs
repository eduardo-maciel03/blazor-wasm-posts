using ApiBlog.Models;
using ApiBlog.Models.Dtos;
using AutoMapper;

namespace ApiBlog.Mappers
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostCriarDto>().ReverseMap();
            CreateMap<Post, PostAtualizarDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();
            CreateMap<Usuario, UsuarioRegistroDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginRespostaDto>().ReverseMap();
        }
    }
}
