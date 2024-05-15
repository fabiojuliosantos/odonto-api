using Microsoft.AspNetCore.Mvc;
using Odonto.API.Models;
using Odonto.API.Services.Interface;

namespace Odonto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistasController : ControllerBase
    {
        IDentistaService _service;
        public DentistasController(IDentistaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Dentista>> BuscarTodosDentistas()
        {
            var dentistas = _service.BuscarTodosDentistas();
            return Ok(dentistas);
        }

        [HttpGet("buscar-dentista-id/{id}")]
        public ActionResult<Dentista> BuscarDentistaPorId(int id)
        {
            var dentista = _service.BuscarPorId(id);
            return Ok(dentista);
        }

        [HttpPost("cadastrar-dentista")]
        public ActionResult<Dentista> CadastrarDentista(Dentista dentista)
        {
            _service.CadastrarDentista(dentista);
            return Ok(dentista);
        }

        [HttpPut("atualizar-dentista")]
        public ActionResult<Dentista> AtualizarDentista(Dentista dentista)
        {
            _service.AtualizarDentista(dentista);
            return Ok(dentista);
        }
        [HttpDelete("excluir-dentista")]
        public ActionResult<Dentista> ExcluirDentista(Dentista dentista)
        {
            _service.ExcluirDentista(dentista);
            return Ok(dentista);
        }
    }
}
