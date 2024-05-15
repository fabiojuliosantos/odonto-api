using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;

namespace Odonto.API.Repositories.Repository;

public class DentistaRepository : Repository<Dentista>, IDentistaRepository
{
    public DentistaRepository(AppDbContext context) : base(context)
    {

    }
}
