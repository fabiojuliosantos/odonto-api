using Odonto.Application.DTO;
using Odonto.Application.Mediator.Consultas.Commands;
using Odonto.Domain.Entities;

namespace Odonto.Application.Interfaces;

public interface IConsultaService
{
    Task<IEnumerable<Consulta>> BuscarTodasConsultasAsync();
    Task<Consulta> BuscarConsultaPorIdAsync(BuscarConsultaPorIdCommand command);
    Task<Consulta> CadastrarConsulta(CadastrarConsultaDTO dto);
    Task<Consulta> AtualizarConsulta(AtualizarConsultaCommand command);
    Task<Consulta> ExcluirConsulta(ExcluirConsultaCommand command);
}
