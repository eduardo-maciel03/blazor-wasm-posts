using ClientBlazorWASM.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace ClientBlazorWASM.Pages.Authentication
{
    public partial class Sair
    {
        [Inject]
        public IServiceAuthentication serviceAuthentication { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await serviceAuthentication.Sair();
            navigationManager.NavigateTo("/");
        }
    }
}
