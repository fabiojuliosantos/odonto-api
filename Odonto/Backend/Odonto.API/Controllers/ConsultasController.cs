﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Consultas;
using Odonto.Application.Interfaces;
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
    public async Task<ActionResult<ConsultasDTO>> BuscarConsultaPorId(int id)
    {
        try
        {
            var consulta = await _service.BuscarConsultaPorIdAsync(id);
            
            if (consulta is null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Consulta não encontrada",
                    Detail = "Consulta com o ID especificado não foi encontrada."
                });
            }

            var consultaDto = _mapper.Map<ConsultasDTO>(consulta);
            
            return Ok(consultaDto);
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
    [HttpPost("cadastrar-consulta")]
    [Authorize]
    public ActionResult<ConsultasCadastroDTO> CadastrarConsulta(ConsultasCadastroDTO consultaDto)
    {
        try
        {
            if(consultaDto is null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Dados não fornecidos",
                    Detail = "Os dados da consulta não foram fornecidos"
                });
            }
            var consulta = _mapper.Map<Consulta>(consultaDto);

            _service.CadastrarConsulta(consulta);

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

    public ActionResult<ConsultasDTO> AtualizarConsulta(ConsultasDTO consultaDto)
    {

        var consulta = _mapper.Map<Consulta>(consultaDto);

        _service.AtualizarConsulta(consulta);

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

    public ActionResult<ConsultasDTO> ExcluirConsulta(ConsultasDTO consultaDto)
    {
        var consulta = _mapper.Map<Consulta>(consultaDto);

        _service.ExcluirConsulta(consulta);

        return Ok(consulta);
    }
    #endregion DELETE
}

