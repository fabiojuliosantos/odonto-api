using Odonto.Blazor.Entities;

namespace Odonto.Blazor.Services;

public class PacientesService
{
    private readonly HttpClient _httpClient;

    public PacientesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Pacientes>> BuscarPacientes() 
    {
        return await _httpClient.GetFromJsonAsync<List<Pacientes>>("api/pacientes");
    }
}
