using ApiBlog.Models;
using ApiBlog.Models.Dtos;

namespace ApiBlog.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int id);
        bool UnicoUsuario(string nome);
        Task<UsuarioLoginRespostaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<Usuario> Registrar(UsuarioRegistroDto usuarioRegistroDto);

    }
}
