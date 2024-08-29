using Odonto.Application.Mediator.Consultas.Commands;
using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using X.PagedList;

namespace Odonto.Application.Interfaces;

public interface IConsultaService
{
    Task<IEnumerable<Consulta>> BuscarTodasConsultasAsync();
    Task<Consulta> BuscarConsultaPorIdAsync(BuscarConsultaPorIdCommand command);
    Task<Consulta> CadastrarConsulta(CadastrarConsultaCommand command);
    Task<Consulta> AtualizarConsulta(AtualizarConsultaCommand command);
    Task<Consulta> ExcluirConsulta(ExcluirConsultaCommand command);
}
