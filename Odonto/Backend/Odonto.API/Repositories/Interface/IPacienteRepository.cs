using Odonto.API.Models;

namespace Odonto.API.Repositories.Interface;

public interface IPacienteRepository : IRepository<Paciente>
{
    Paciente BuscarPacientePeloIdConsulta(int id);
}
