using Microsoft.EntityFrameworkCore;
using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;
using System.Linq.Expressions;

namespace Odonto.API.Repositories.Repository;

public class PacienteRepository : Repository<Paciente>, IPacienteRepository
{
    public PacienteRepository(AppDbContext context) : base(context)
    {
    }

}
