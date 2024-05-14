using Microsoft.AspNetCore.Mvc;
using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Services.Interface;

namespace Odonto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
       
        private readonly IPacienteService _service;
        public PacientesController(AppDbContext context, IPacienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Paciente>> BuscarTodosPacientes()
        {
            var pacientes = _service.BuscarTodosPacientes();
            if (pacientes is null) return NotFound();
            return Ok(pacientes);
        }

        [HttpGet("buscar-paciente-id/{id}")]
        public ActionResult<Paciente> BuscarPacientePorId(int id)
        {
            var paciente = _service.BuscarPacientePorId(id);
            if (paciente is null) return NotFound();
            return Ok(paciente);
        }

        [HttpPost("cadastrar-paciente")]
        public ActionResult<Paciente> CadastrarPaciente(Paciente paciente)
        {
            _service.CadastrarPaciente(paciente);
            return Ok(paciente);
        }

        [HttpPut("atualizar-paciente")]
        public ActionResult<Paciente> AtualizarPaciente(Paciente paciente)
        {
            _service.AtualizarPaciente(paciente);
            return Ok(paciente);
        }

        [HttpDelete("excluir-paciente/{id}")]
        public ActionResult<Paciente>ExcluirPaciente(int id)
        {
            _service.ExcluirPaciente(id);
            return Ok($"Paciente de id: {id} excluído com sucesso!");
        }
    }
}
