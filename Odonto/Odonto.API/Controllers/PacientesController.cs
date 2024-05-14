using Microsoft.AspNetCore.Mvc;
using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;
using Odonto.API.Repositories.Repository;

namespace Odonto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPacienteRepository _repository;
        public PacientesController(AppDbContext context, IPacienteRepository repository)
        {
            _context = context;
            _repository = repository;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Paciente>> BuscarTodosPacientes()
        {
            var pacientes = _repository.BuscarTodosPacientes();

            if (pacientes is null) return NotFound();

            return Ok(pacientes);
        }
    }
}
