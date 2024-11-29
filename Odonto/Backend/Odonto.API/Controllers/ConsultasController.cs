using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Consultas;
using Odonto.Application.Interfaces;
using Odonto.Application.Mediator.Consultas.Commands;
using Odonto.Domain.Entities;

namespace Odonto.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]

public class ConsultasController : ControllerBase
{
    #region MEMBROS

    private readonly IMapper _mapper;
    private readonly IConsultaService _service;

    #endregion MEMBROS

    #region CONSTRUTOR
    public ConsultasController(IConsultaService service,
                               IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    #endregion CONSTRUTOR

    #region GET
    /// <summary>
    /// Busca todas consultas cadastradas
    /// </summary>
    /// <returns>Retorna todos os objetos das consultas cadastradas</returns>
    [HttpGet("buscar-consultas")]
    public async Task<ActionResult<ConsultasDTO>> BuscarTodasConsultas()
    {
        var consultas = await _service.BuscarTodasConsultasAsync();

        var consultasDto = _mapper.Map<IEnumerable<ConsultasDTO>>(consultas);

        return Ok(consultasDto);
    }

    /// <summary>
    /// Busca uma consulta específica pelo ID
    /// </summary>
    /// <param name="id">ID da consulta que será buscada</param>
    /// <returns>Retorna o objeto da consulta</returns>
    [HttpGet("buscar-consulta-id/{id}")]
    public async Task<ActionResult> BuscarConsultaPorId(int id)
    {
        try
        {
            BuscarConsultaPorIdCommand command = new BuscarConsultaPorIdCommand() { ConsultaId = id};

            Consulta consulta = await _service.BuscarConsultaPorIdAsync(command);
            
            if (consulta is null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Consulta não encontrada",
                    Detail = "Consulta com o ID especificado não foi encontrada."
                });
            }
            
            return Ok(consulta);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erro ao buscar consulta",
                Detail = ex.Message
            });
        }
    }
    #endregion GET

    #region POST
    /// <summary>
    /// Cadastra uma nova consulta [Endpoint Protegido]
    /// </summary>
    /// <param name="consultaDto">Objeto da consulta que será cadastrada</param>
    /// <returns>Retorna o objeto da consulta cadastrada</returns>
    [Authorize(Policy = "Secretaria")]
    [HttpPost("cadastrar-consulta")]
    public async Task<ActionResult> CadastrarConsulta(CadastrarConsultaCommand command)
    {
        try
        {
            if(command is null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Dados não fornecidos",
                    Detail = "Os dados da consulta não foram fornecidos"
                });
            }
            Consulta consulta = await _service.CadastrarConsulta(command);

            return Ok(consulta);
        }
        catch (Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erro ao cadastrar consulta",
                Detail = ex.Message
            });
        }
    }

    #endregion POST

    #region PUT
    /// <summary>
    /// Atualiza uma consulta específica [Endpoint Protegido]
    /// </summary>
    /// <param name="consultaDto">Objeto da consulta que será atualizada</param>
    /// <returns>Retorna o objeto da consulta atualizada</returns>
    [HttpPut("atualizar-consulta")]
    [Authorize(Policy = "Admin")]

    public async Task<ActionResult> AtualizarConsulta(AtualizarConsultaCommand command)
    {
        Consulta consulta = await _service.AtualizarConsulta(command);

        return Ok(consulta);
    }

    #endregion PUT

    #region DELETE
    /// <summary>
    /// Deleta uma consulta específica [Endpoint Protegido]
    /// </summary>
    /// <param name="consultaDto">Objeto da consulta que será Deletada</param>
    /// <returns>Retorna o objeto da consulta Deletada</returns>
    [HttpDelete("excluir-consulta")]
    [Authorize(Policy = "Admin")]

    public async Task<ActionResult> ExcluirConsulta(int id)
    {
        ExcluirConsultaCommand command = new ExcluirConsultaCommand() { ConsultaId = id };

        await _service.ExcluirConsulta(command);

        return Ok($"Consulta {id} excluída com sucesso!");
    }
    #endregion DELETE
}

