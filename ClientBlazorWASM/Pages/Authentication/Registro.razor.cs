using ClientBlazorWASM.Models;
using ClientBlazorWASM.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace ClientBlazorWASM.Pages.Authentication
{
    public partial class Registro
    {
        private UsuarioRegistro UsuarioParaRegistro = new UsuarioRegistro();
        public bool Processando { get; set; } = false;
        public bool ErrosRegistro { get; set; }
        public IEnumerable<string> Erros { get; set; }

        [Inject]
        public IServiceAuthentication serviceAuthentication { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task RegistrarUsuario()
        {
            ErrosRegistro = false;
            Processando = true;
            var result = await serviceAuthentication.RegistrarUsuario(UsuarioParaRegistro);
            if (result.registroCorreto)
            {
                Processando = false;
                navigationManager.NavigateTo("/acessar");
            }
            else
            {
                Processando = false;
                Erros = result.Erros;
                ErrosRegistro = true;
            }
        }
    }
}
