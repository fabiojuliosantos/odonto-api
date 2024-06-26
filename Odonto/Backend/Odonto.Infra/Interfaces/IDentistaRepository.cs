using Odonto.Domain.Entities;

namespace Odonto.Infra.Interfaces;

public interface IDentistaRepository : IRepository<Dentista>
{
    Task<Dentista> BuscarDentistaPeloIdConsultaAsync(int id);
}