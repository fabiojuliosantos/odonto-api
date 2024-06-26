using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using X.PagedList;

namespace Odonto.Infra.Interfaces;

public interface IConsultaRepository : IRepository<Consulta>
{
    Task<Consulta> BuscarConsultaComPacienteDentistaPorIdAsync(int id);
    Task<IPagedList<Consulta>> BuscarConsultasPaginadas(ConsultasParameters param);
}