using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Pacientes;
using Odonto.Application.Interfaces;
using Odonto.Domain.Entities;

namespace Odonto.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]


public class PacientesController : ControllerBase
{
    #region MEMBROS    

    private readonly IMapper _mapper;
    private readonly IPacienteService _service;

    #endregion MEMBROS

    #region CONSTRUTOR
    public PacientesController(IMapper mapper, 
                               IPacienteService service)
    {
        _service = service;
        _mapper = mapper;
    }
    #endregion CONSTRUTOR

    #region GET
    /// <summary>
    /// Busca todos Pacientes
    /// </summary>
    /// <returns>Retorna um array com todos os objetos de clientes.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Paciente>>> BuscarTodosPacientes()
    {
        var pacientes = await _service.BuscarTodosPacientesAsync();

        if (pacientes is null) return NotFound();

        return Ok(pacientes);
    }

    /// <summary>
    /// Busca o paciente específico pelo ID
    /// </summary>
    /// <param name="id">ID do paciente que irá buscar</param>
    /// <returns></returns>
    [HttpGet("buscar-paciente-id/{id}")] //Consulta pois não altera o estado
    public async Task<ActionResult<Paciente>> BuscarPacientePorId(int id)
    {
        var paciente = await _service.BuscarPacientePorIdAsync(id);

        if (paciente is null) return NotFound();

        return Ok(paciente);
    }

    #endregion GET

    #region POST

    /// <summary>
    /// Cadastra um novo paciente [Endpoint Protegido]
    /// </summary>
    /// <param name="pacienteDto">Objeto de Paciente</param>
    /// <returns>Retorna o objeto do paciente cadastrado</returns>
    //[Authorize]
    [HttpPost("cadastrar-paciente")] //Comando pois altera os estados
    public ActionResult<PacientesCadastroDTO> CadastrarPaciente(PacientesCadastroDTO pacienteDto)
    {
        var paciente = _mapper.Map<Paciente>(pacienteDto);

        _service.CadastrarPaciente(paciente);

        return Ok(paciente);
    }

    #endregion POST

    #region PUT

    /// <summary>
    /// Atualiza os dados cadastrais de um paciente [Endpoint Protegido]
    /// </summary>
    /// <param name="pacienteDto">Objeto do Paciente</param>
    /// <returns>Retorna o objeto do paciente</returns>
    [Authorize]
    [HttpPut("atualizar-paciente")]
    public ActionResult<PacientesDTO> AtualizarPaciente(PacientesDTO pacienteDto)
    {
        var paciente = _mapper.Map<Paciente>(pacienteDto);

        _service.AtualizarPaciente(paciente);

        return Ok(paciente);
    }
    #endregion PUT

    #region DELETE

    /// <summary>
    /// Deleta o paciente cadastrado [Endpoint Protegido]
    /// </summary>
    /// <param name="pacienteDto">Objeto do Paciente</param>
    /// <returns>Retorna o objeto do paciente</returns>
    [Authorize]
    [HttpDelete("excluir-paciente/")]
    public ActionResult<PacientesDTO> ExcluirPaciente(int id)
    {
        //var paciente = _mapper.Map<Paciente>(pacienteDto);

        _service.ExcluirPaciente(id);

        return Ok($"Paciente {id}, excluído com sucesso!");
    }

    #endregion DELETE
}