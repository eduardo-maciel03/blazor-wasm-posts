using Blazored.LocalStorage;
using ClientBlazorWASM.Helpers;
using ClientBlazorWASM.Models;
using ClientBlazorWASM.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ClientBlazorWASM.Services
{
    public class ServiceAuthentication : IServiceAuthentication
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationState;

        public ServiceAuthentication(HttpClient client, 
            ILocalStorageService localStorageService, 
            AuthenticationStateProvider authenticationState)
        {
            _client = client;
            _localStorageService = localStorageService;
            _authenticationState = authenticationState;
        }
        public async Task<RespostaAutenticacao> Acessar(UsuarioAutenticacao autenticarUsuario)
        {
            var content = JsonConvert.SerializeObject(autenticarUsuario);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{Constants.UrlBaseApi}api/usuarios/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = (JObject)JsonConvert.DeserializeObject(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                var Token = result["result"]["token"].Value<string>();
                var Usuario = result["result"]["usuario"]["nomeUsuario"].Value<string>();

                await _localStorageService.SetItemAsync(Constants.Token_Local, Token);
                await _localStorageService.SetItemAsync(Constants.Usuario_Local, Usuario);
                ((AuthStateProvider)_authenticationState).NotificarUsuarioLogado(Token);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                return new RespostaAutenticacao { IsSuccess = true };
            }
            else
            {
                return new RespostaAutenticacao { IsSuccess = false };
            }
        }

        public async Task<RespostaRegistro> RegistrarUsuario(UsuarioRegistro registrarUsuario)
        {
            var content = JsonConvert.SerializeObject(registrarUsuario);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{Constants.UrlBaseApi}api/usuarios/registro", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RespostaRegistro>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RespostaRegistro { registroCorreto = true };
            }
            else
            {
                return result;
            }
        }

        public async Task Sair()
        {
            await _localStorageService.RemoveItemAsync(Constants.Token_Local);
            await _localStorageService.RemoveItemAsync(Constants.Usuario_Local);
            ((AuthStateProvider)_authenticationState).NotificarUsuarioDeslogado();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
