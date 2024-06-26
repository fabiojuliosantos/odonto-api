using Microsoft.EntityFrameworkCore;
using Odonto.Domain.Entities;
using Odonto.Infra.Context;
using Odonto.Infra.Interfaces;

namespace Odonto.Infra.Repositories;

public class DentistaRepository : Repository<Dentista>, IDentistaRepository
{
    public DentistaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Dentista> BuscarDentistaPeloIdConsultaAsync(int id)
    {
        var dentista = await _context.Dentistas.Include(d => d.Consultas)
            .FirstOrDefaultAsync(d => d.DentistaId == id);
        return dentista;
    }
}