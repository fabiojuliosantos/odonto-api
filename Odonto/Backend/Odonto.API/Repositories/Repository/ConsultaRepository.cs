using Microsoft.EntityFrameworkCore;
using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Odonto.API.Repositories.Repository;

public class ConsultaRepository : Repository<Consulta>, IConsultaRepository
{

    public ConsultaRepository(AppDbContext context) : base(context)
    {
    }

    public Consulta BuscarConsultaComPacienteDentistaPorId(int id)
    {
        Consulta consulta = _context.Consultas
                            .Include(c => c.Paciente)
                            .Include(c => c.Dentista)
                            .FirstOrDefault(c => c.ConsultaId == id);
        return consulta;
    }
}
