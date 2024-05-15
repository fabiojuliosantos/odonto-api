using Odonto.API.Models;

namespace Odonto.API.Repositories.Interface;

public interface IConsultaRepository : IRepository<Consulta>
{
    Consulta BuscarConsultaComPacienteDentistaPorId(int id);
}
