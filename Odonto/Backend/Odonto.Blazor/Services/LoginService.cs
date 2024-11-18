using Blazored.LocalStorage;
using Microsoft.JSInterop;
using Odonto.Blazor.Entities;
using System.Net.Http.Json;

namespace Odonto.Blazor.Services
{
    public class LoginService
    {
        private readonly HttpClient _client;
        private readonly IJSRuntime _jsRuntime;
        private string _jwt;

        public LoginService(HttpClient client, IJSRuntime jsRuntime)
        {
            _client = client;
            _jsRuntime = jsRuntime;
        }

        public async Task<string> Logar(string username, string password)
        {
            LoginModel login = new LoginModel()
            {
                username = username,
                password = password
            };

            var response = await _client.PostAsJsonAsync("api/Autenticacao/login", login);

            if (response.IsSuccessStatusCode)
            {
                _jwt = await response.Content.ReadAsStringAsync();
                return _jwt;
            }

            return "erro";
        }

        public async Task ArmazenarTokenAsync()
        {
            if (!string.IsNullOrEmpty(_jwt))
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", _jwt);
            }
        }
    }
}
