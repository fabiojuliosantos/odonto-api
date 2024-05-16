using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Odonto.API.DTOs.Consultas;
using Odonto.API.Models;
using Odonto.API.Pagination;
using Odonto.API.Services.Interface;

namespace Odonto.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsultasController : ControllerBase
{
    #region MEMBROS
    
    private readonly IMapper _mapper;
    private readonly IConsultaService _service;
    
    #endregion MEMBROS
    
    #region CONSTRUTOR
    public ConsultasController(IConsultaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    #endregion CONSTRUTOR
    
    #region GET

    [HttpGet]
    public async Task<ActionResult<ConsultasDTO>> BuscarTodasConsultas()
    {
        var consultas = await _service.BuscarTodasConsultasAsync();

        var consultasDto = _mapper.Map<IEnumerable<ConsultasDTO>>(consultas);

        return Ok(consultasDto);
    }

    [HttpGet("buscar-consulta-id/{id}")]
    public async Task<ActionResult<ConsultasDTO>> BuscarConsultaPorId(int id)
    {
        var consulta = await _service.BuscarConsultaPorIdAsync(id);

        var consultaDto = _mapper.Map<ConsultasDTO>(consulta);

        return Ok(consultaDto);
    }

    [HttpGet("buscar-consultas-paginadas")]
    public async Task<ActionResult<Consulta>> BuscarConsultasPaginadas([FromQuery] ConsultasParameters param)
    {
        var consultasPaginadas = await _service.BuscarConsultasPaginadas(param);
        var metadata =
            new
            {
                consultasPaginadas.PageCount,
                consultasPaginadas.PageSize,
                consultasPaginadas.Count,
                consultasPaginadas.TotalItemCount,
                consultasPaginadas.HasNextPage,
                consultasPaginadas.HasPreviousPage
            };
        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        return Ok(consultasPaginadas);
    }

    #endregion GET

    #region POST

    [HttpPost("cadastrar-consulta")]
    public ActionResult<ConsultasCadastroDTO> CadastrarConsulta(ConsultasCadastroDTO consultaDto)
    {
        var consulta = _mapper.Map<Consulta>(consultaDto);

        _service.CadastrarConsulta(consulta);

        return Ok(consulta);
    }

    #endregion POST

    #region PUT

    [HttpPut("atualizar-consulta")]
    public ActionResult<ConsultasDTO> AtualizarConsulta(ConsultasDTO consultaDto)
    {
        var consulta = _mapper.Map<Consulta>(consultaDto);

        _service.AtualizarConsulta(consulta);

        return Ok(consulta);
    }

    #endregion PUT

    #region DELETE

    [HttpDelete("excluir-consulta")]
    public ActionResult<ConsultasDTO> ExcluirConsulta(ConsultasDTO consultaDto)
    {
        var consulta = _mapper.Map<Consulta>(consultaDto);

        _service.ExcluirConsulta(consulta);

        return Ok(consulta);
    }
    #endregion DELETE
}

