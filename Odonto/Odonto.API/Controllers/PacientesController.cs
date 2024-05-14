using Microsoft.AspNetCore.Mvc;
using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;
using Odonto.API.Repositories.Repository;
using Odonto.API.Services.Interface;

namespace Odonto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPacienteService _service;
        public PacientesController(AppDbContext context, IPacienteService service)
        {
            _context = context;
            _service = service;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Paciente>> BuscarTodosPacientes()
        {
            var pacientes = _service.BuscarTodosPacientes();

            if (pacientes is null) return NotFound();

            return Ok(pacientes);
        }
    }
}
