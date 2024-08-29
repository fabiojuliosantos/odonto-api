using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using X.PagedList;

namespace Odonto.Application.Interfaces;

public interface IConsultaService
{
    Task<IEnumerable<Consulta>> BuscarTodasConsultasAsync();
    Task<Consulta> BuscarConsultaPorIdAsync(int id);
    Task<Consulta> CadastrarConsulta(Consulta consulta);
    Task<Consulta> AtualizarConsulta(Consulta consulta);
    Task<Consulta> ExcluirConsulta(int id);
}
