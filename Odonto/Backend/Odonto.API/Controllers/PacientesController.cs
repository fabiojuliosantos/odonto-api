using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Odonto.API.DTOs.Pacientes;
using Odonto.API.Models;
using Odonto.API.Pagination;
using Odonto.API.Services.Interface;

namespace Odonto.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacientesController : ControllerBase
{
    #region MEMBROS    
    
    private readonly IMapper _mapper;
    private readonly IPacienteService _service;
    
    #endregion MEMBROS
    
    #region CONSTRUTOR
    public PacientesController(IMapper mapper, IPacienteService service)
    {
        _service = service;
        _mapper = mapper;
    }
    #endregion CONSTRUTOR
    
    #region GET
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Paciente>>> BuscarTodosPacientes()
    {
        var pacientes = await _service.BuscarTodosPacientesAsync();

        if (pacientes is null) return NotFound();

        return Ok(pacientes);
    }

    [HttpGet("buscar-paciente-id/{id}")]
    public async Task<ActionResult<Paciente>> BuscarPacientePorId(int id)
    {
        var paciente = await _service.BuscarPacientePorIdAsync(id);

        if (paciente is null) return NotFound();

        return Ok(paciente);
    }

    [HttpGet("pacientes-paginados")]
    public async Task<ActionResult<Paciente>> BuscarPacientesPaginados([FromQuery] PacientesParameters param)
    {
        var pacientesPaginados = await _service.PacientesPaginadosAsync(param);

        var metadata = new
        {
            pacientesPaginados.PageCount,
            pacientesPaginados.PageSize,
            pacientesPaginados.PageNumber,
            pacientesPaginados.TotalItemCount,
            pacientesPaginados.HasNextPage,
            pacientesPaginados.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        return Ok(pacientesPaginados);
    }
    
    #endregion GET
    
    #region POST
    [HttpPost("cadastrar-paciente")]
    public ActionResult<PacientesCadastroDTO> CadastrarPaciente(PacientesCadastroDTO pacienteDto)
    {
        var paciente = _mapper.Map<Paciente>(pacienteDto);

        _service.CadastrarPaciente(paciente);

        return Ok(paciente);
    }
    #endregion POST
    
    #region PUT
    
    [HttpPut("atualizar-paciente")]
    public ActionResult<PacientesDTO> AtualizarPaciente(PacientesDTO pacienteDto)
    {
        var paciente = _mapper.Map<Paciente>(pacienteDto);

        _service.AtualizarPaciente(paciente);

        return Ok(paciente);
    }
    #endregion PUT
    
    #region DELETE
    
    [HttpDelete("excluir-paciente/")]
    public ActionResult<PacientesDTO> ExcluirPaciente(PacientesDTO pacienteDto)
    {
        var paciente = _mapper.Map<Paciente>(pacienteDto);

        _service.ExcluirPaciente(paciente);

        return Ok($"Paciente {paciente.Nome}, excluído com sucesso!");
    }
    
    #endregion DELETE
}