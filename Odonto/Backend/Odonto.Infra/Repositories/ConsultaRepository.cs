using Microsoft.EntityFrameworkCore;
using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using Odonto.Infra.Context;
using Odonto.Infra.Interfaces;
using X.PagedList;

namespace Odonto.Infra.Repositories;

public class ConsultaRepository : Repository<Consulta>, IConsultaRepository
{
    public ConsultaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Consulta> BuscarConsultaComPacienteDentistaPorIdAsync(int id)
    {
        var consulta = await _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Dentista)
            .FirstOrDefaultAsync(c => c.ConsultaId == id);
        return consulta;
    }

    public async Task<IPagedList<Consulta>> BuscarConsultasPaginadas(ConsultasParameters param)
    {
        var consultasPaginadas = await BuscarTodosAsync();

        var consultasOrdenadas = consultasPaginadas.OrderBy(c => c.ConsultaId)
                                                   .AsQueryable();  
        
        var consultasRetorno = await consultasPaginadas.ToPagedListAsync(param.PageNumber, param.PageSize);
        
        return consultasRetorno;
    }

}