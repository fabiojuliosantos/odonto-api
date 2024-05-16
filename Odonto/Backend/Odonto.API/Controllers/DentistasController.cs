using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Dentistas;
using Odonto.API.Models;
using Odonto.API.Services.Interface;

namespace Odonto.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DentistasController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDentistaService _service;

    public DentistasController(IDentistaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dentista>>> BuscarTodosDentistas()
    {
        var dentistas = await _service.BuscarTodosDentistasAsync();
        return Ok(dentistas);
    }

    [HttpGet("buscar-dentista-id/{id}")]
    public async Task<ActionResult<Dentista>> BuscarDentistaPorId(int id)
    {
        var dentista = await _service.BuscarPorIdAsync(id);
        return Ok(dentista);
    }

    [HttpPost("cadastrar-dentista")]
    public ActionResult<DentistasCadastroDTO> CadastrarDentista(DentistasCadastroDTO dentistaDto)
    {
        var dentista = _mapper.Map<Dentista>(dentistaDto);
        _service.CadastrarDentista(dentista);
        return Ok(dentista);
    }

    [HttpPut("atualizar-dentista")]
    public ActionResult<DentistasDTO> AtualizarDentista(DentistasDTO dentistaDto)
    {
        var dentista = _mapper.Map<Dentista>(dentistaDto);

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