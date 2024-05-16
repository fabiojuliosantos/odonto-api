using Odonto.API.Models;

namespace Odonto.API.Repositories.Interface;

public interface IDentistaRepository : IRepository<Dentista>
{
    Task<Dentista> BuscarDentistaPeloIdConsultaAsync(int id);
}