using Odonto.API.Models;

namespace Odonto.API.Repositories.Interface;

public interface IDentistaRepository : IRepository<Dentista>
{
    Dentista BuscarDentistaPeloIdConsulta(int id);
}
