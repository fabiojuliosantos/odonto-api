using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using X.PagedList;

namespace Odonto.Application.Interfaces;

public interface IConsultaService
{
    Task<IEnumerable<Consulta>> BuscarTodasConsultasAsync();
    Task<IPagedList<Consulta>> BuscarConsultasPaginadas(ConsultasParameters param);
    Task<Consulta> BuscarConsultaPorIdAsync(int id);
    Consulta CadastrarConsulta(Consulta consulta);
    Consulta AtualizarConsulta(Consulta consulta);
    Consulta ExcluirConsulta(Consulta consulta);
}
