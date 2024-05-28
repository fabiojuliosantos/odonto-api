using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.AuthenticationFilter;
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
    [Authorize]
    public async Task<ActionResult<Dentista>> BuscarDentistaPorId(int id)
    {
        var dentista = await _service.BuscarPorIdAsync(id);
        return Ok(dentista);
    }

    [HttpPost("cadastrar-dentista")]
    [Authorize(Policy = "Admin")]
    [VerifyUserHasPolicy("Admin")]
    public ActionResult<DentistasCadastroDTO> CadastrarDentista(DentistasCadastroDTO dentistaDto)
    {
        try
        {
            var dentista = _mapper.Map<Dentista>(dentistaDto);
            _service.CadastrarDentista(dentista);
            return Ok(dentista);
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    [HttpPut("atualizar-dentista")]
    [Authorize(Policy = "Admin")]
    public ActionResult<DentistasDTO> AtualizarDentista(DentistasDTO dentistaDto)
    {
        var dentista = _mapper.Map<Dentista>(dentistaDto);

        _service.AtualizarDentista(dentista);

        return Ok(dentista);
    }
    
    [HttpDelete("excluir-dentista")]
    [Authorize(Policy = "Admin")]
    public ActionResult<Dentista> ExcluirDentista(Dentista dentista)
    {
        _service.ExcluirDentista(dentista);
        return Ok(dentista);
    }
}