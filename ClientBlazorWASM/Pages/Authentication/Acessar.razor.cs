using ClientBlazorWASM.Models;
using ClientBlazorWASM.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace ClientBlazorWASM.Pages.Authentication
{
    public partial class Acessar
    {
        private UsuarioAutenticacao UsuarioAutenticacao = new UsuarioAutenticacao();
        public bool Processando { get; set; } = false;
        public bool ErrosAutenticacao { get; set; }
        public string UrlRetorno { get; set; }
        public string Erros { get; set; }

        [Inject]
        public IServiceAuthentication serviceAuthentication { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task AcessoUsuario()
        {
            ErrosAutenticacao = false;
            Processando = true;
            var result = await serviceAuthentication.Acessar(UsuarioAutenticacao);
            if (result.IsSuccess)
            {
                Processando = false;
                var url = new Uri(navigationManager.Uri);
                var parametrosQuery = HttpUtility.ParseQueryString(url.Query);
                UrlRetorno = parametrosQuery["returnUrl"];
                if (string.IsNullOrEmpty(UrlRetorno))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + UrlRetorno);
                }
                
            }
            else
            {
                Processando = false;
                ErrosAutenticacao = true;
                Erros = "Usuário ou senha incorretos!";
            }
        }
    }
}
