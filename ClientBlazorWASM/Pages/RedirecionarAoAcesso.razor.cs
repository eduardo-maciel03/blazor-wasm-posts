using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClientBlazorWASM.Pages
{
    public partial class RedirecionarAoAcesso
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> estadoAutenticacao { get; set; }
        bool naoAutorizado { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            var estadoAutorizacao = await estadoAutenticacao;

            if(estadoAutorizacao.User == null) 
            {
                var returnUrl = navigationManager.ToBaseRelativePath(navigationManager.Uri);
                if(string.IsNullOrEmpty(returnUrl)) 
                {
                    navigationManager.NavigateTo("Acessar", true);
                }
                else
                {
                    navigationManager.NavigateTo($"Acessar?returnUrl={returnUrl}", false);
                }
            }
            else
            {
                naoAutorizado = true;
            }
        }
    }
}
