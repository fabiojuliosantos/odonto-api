using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;

namespace Odonto.API.Repositories.Repository;

public class ConsultaRepository : Repository<Consulta>, IConsultaRepository
{
    public ConsultaRepository(AppDbContext context) : base(context)
    {
    }
}
