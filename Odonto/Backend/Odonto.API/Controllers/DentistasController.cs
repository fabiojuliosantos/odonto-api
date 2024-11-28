using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Dentistas;
using Odonto.API.RabbitMQSender;
using Odonto.Application.Interfaces;
using Odonto.Application.Mediator.Dentistas.Commands;
using Odonto.Domain.Entities;
using Odonto.Application.TratarErros;


namespace Odonto.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]

public class DentistasController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDentistaService _service;
    private readonly ILogger<DentistasController> _logger;
    private readonly IRabbitMQMessageSender _rabbitMqMessageSender;
    public DentistasController(IDentistaService service,
                               IMapper mapper,
                               ILogger<DentistasController> logger,
                               IRabbitMQMessageSender rabbitMqMessageSender)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
        _rabbitMqMessageSender = rabbitMqMessageSender;
    }

    /// <summary>
    /// Busca todos dentistas cadastrados
    /// </summary>
    /// <returns>Retorna os objetos de todos dentistas cadastrados</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dentista>>> BuscarTodosDentistas()
    {
        try
        {
            var dentistas = await _service.BuscarTodosDentistasAsync();
            return Ok(dentistas);
        }
        catch (CustomException ex) 
        {
            _logger.LogError(ex.StatusCode, ex.Message);
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }

    /// <summary>
    /// Busca um dentista específico pelo ID
    /// </summary>
    /// <param name="id">ID do dentista que irá buscar</param>
    /// <returns>Retorna o objeto do dentista encontrado</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> BuscarDentistaPorId(int id)
    {
        try
        {
            BuscarDentistaPorIdCommand command = new BuscarDentistaPorIdCommand() { DentistaId = id };
            Dentista dentista = await _service.BuscarPorIdAsync(command);
            return Ok(dentista);
        }
        catch (CustomException ex) 
        {
            _logger.LogError(ex.StatusCode, ex.Message);
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }

    /// <summary>
    /// Cadastra um novo dentista [Endpoint Protegido]
    /// </summary>
    /// <param name="dentistaDto">Objeto do dentista que será cadastrado</param>
    /// <returns>Retorna o objeto do dentista cadastrado</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("")]
    [Authorize]
    public async Task<ActionResult> CadastrarDentista(CadastrarDentistaCommand command)
    {
        try
        {
            Dentista dentista = await _service.CadastrarDentista(command);
            
            _logger.LogTrace($"Dentista {dentista.Nome} cadastrado com sucesso!");
            return Ok(dentista);
        }
        catch (CustomException ex)
        {
            _logger.LogError(ex.StatusCode, ex.Message);
            return StatusCode(ex.StatusCode, new {message = ex.Message});
        }
    }
    [HttpPost("cadastrar-dentista-fila")]
    public async Task<ActionResult> CadastrarDentistaFila(DentistasCadastroDTO dentista)
    {
        try
        {
            _rabbitMqMessageSender.SendMessage(dentista, "cadastroDentista");
            return Ok(dentista);
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
    }

    /// <summary>
    /// Atualiza um dentista específico [Endpoint Protegido]
    /// </summary>
    /// <param name="dentistaDto">Objeto do dentista que será atualizado</param>
    /// <returns>Retorna o objeto do dentista atualizado</returns>
    /// <exception cref="Exception"></exception>
    [HttpPut("")]
    [Authorize]
    public async Task<ActionResult> AtualizarDentista(AtualizarDentistaCommand command)
    {
        try 
        {
            Dentista dentista = await _service.AtualizarDentista(command);
            _logger.LogTrace($"Dentista {dentista.Nome} atualizado com sucesso");
            return Ok(dentista);
        }
        catch (CustomException ex) 
        {
            _logger.LogError(ex.StatusCode, ex.Message);
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deleta um dentista específico [Endpoint Protegido]
    /// </summary>
    /// <param name="dentistaDto">Objeto do dentista que será deeltado</param>
    /// <returns>Retorna o objeto do dentista deletado</returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("")]
    [Authorize]
    public async Task<ActionResult> ExcluirDentista(ExcluirDentistaCommand command)
    {
        try
        {
            Dentista dentista = await _service.ExcluirDentista(command);
            _logger.LogTrace($"Dentista {dentista.Nome} excluído com sucesso");
            return Ok(dentista);
        }
        catch(CustomException ex)
        {
            _logger.LogError(ex.StatusCode, ex.Message);
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }
}