using Microsoft.EntityFrameworkCore;
using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;

namespace Odonto.API.Repositories.Repository;

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