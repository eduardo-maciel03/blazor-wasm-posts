using ClientBlazorWASM.Models;

namespace ClientBlazorWASM.Services.Interface
{
    public interface IServiceAuthentication
    {
        Task<RespostaRegistro> RegistrarUsuario(UsuarioRegistro registrarUsuario);
        Task<RespostaAutenticacao> Acessar(UsuarioAutenticacao autenticarUsuario);
        Task Sair();
    }
}
