using Odonto.Domain.Entities;

namespace Odonto.Infra.Interfaces;

public interface IConsultaRepository
{
    Task<IEnumerable<Consulta>> BuscarTodasConsultas();

    Task<IEnumerable<Consulta>> BuscarConsultaPorPaciente(int pacienteId);
    Task<IEnumerable<Consulta>> BuscarConsultaPorDentista(int dentistaId);
    Task<Consulta> BuscarConsultaPorId(int id);
    Task<Consulta> CadastrarConsulta(Consulta consulta);
    Task<Consulta> AtualizarConsulta(Consulta consulta);
    Task<Consulta> ExcluirConsulta(int id);
}