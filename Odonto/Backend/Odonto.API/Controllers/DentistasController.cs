using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.AuthenticationFilter;
using Odonto.API.DTOs.Dentistas;
using Odonto.Application.Interfaces;
using Odonto.Domain.Entities;

namespace Odonto.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]

public class DentistasController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDentistaService _service;

    public DentistasController(IDentistaService service, 
                               IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Busca todos dentistas cadastrados
    /// </summary>
    /// <returns>Retorna os objetos de todos dentistas cadastrados</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dentista>>> BuscarTodosDentistas()
    {
        var dentistas = await _service.BuscarTodosDentistasAsync();
        return Ok(dentistas);
    }

    /// <summary>
    /// Busca um dentista específico pelo ID
    /// </summary>
    /// <param name="id">ID do dentista que irá buscar</param>
    /// <returns>Retorna o objeto do dentista encontrado</returns>
    [HttpGet("buscar-dentista-id/{id}")]
    public async Task<ActionResult<Dentista>> BuscarDentistaPorId(int id)
    {
        var dentista = await _service.BuscarPorIdAsync(id);
        return Ok(dentista);
    }

    /// <summary>
    /// Cadastra um novo dentista [Endpoint Protegido]
    /// </summary>
    /// <param name="dentistaDto">Objeto do dentista que será cadastrado</param>
    /// <returns>Retorna o objeto do dentista cadastrado</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("cadastrar-dentista")]
    [Authorize]
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
            throw;
        }
    }

    /// <summary>
    /// Atualiza um dentista específico [Endpoint Protegido]
    /// </summary>
    /// <param name="dentistaDto">Objeto do dentista que será atualizado</param>
    /// <returns>Retorna o objeto do dentista atualizado</returns>
    /// <exception cref="Exception"></exception>
    [HttpPut("atualizar-dentista")]
    [Authorize]
    public ActionResult<DentistasDTO> AtualizarDentista(DentistasDTO dentistaDto)
    {
        var dentista = _mapper.Map<Dentista>(dentistaDto);

        _service.AtualizarDentista(dentista);

        return Ok(dentista);
    }

    /// <summary>
    /// Deleta um dentista específico [Endpoint Protegido]
    /// </summary>
    /// <param name="dentistaDto">Objeto do dentista que será deeltado</param>
    /// <returns>Retorna o objeto do dentista deletado</returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("excluir-dentista")]
    [Authorize]
    public ActionResult<Dentista> ExcluirDentista(Dentista dentista)
    {
        _service.ExcluirDentista(dentista);
        return Ok(dentista);
    }
}