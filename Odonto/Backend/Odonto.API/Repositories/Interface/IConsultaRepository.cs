using Odonto.API.Models;
using Odonto.API.Pagination;
using X.PagedList;

namespace Odonto.API.Repositories.Interface;

public interface IConsultaRepository : IRepository<Consulta>
{
    Task<Consulta> BuscarConsultaComPacienteDentistaPorIdAsync(int id);
    Task<IPagedList<Consulta>> BuscarConsultasPaginadas(ConsultasParameters param);
}