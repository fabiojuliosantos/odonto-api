using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Documentos;
using Odonto.Application.Interfaces;
using Odonto.Application.TratarErros;
using StackExchange.Redis;
using System.Security.Claims;

namespace Odonto.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentosController : ControllerBase
{
    private readonly IDocumentosService _service;
    private readonly ILogger<DocumentosController> _logger;
    private readonly IAtestadosMessageSender _atestadosMessageSender;
    private readonly IReceitasMessageSender _receitasMessageSender;
    private readonly IConnectionMultiplexer _redis;
    private readonly IMapper _mapper;

    public DocumentosController(IDocumentosService service, ILogger<DocumentosController> logger,
                                IAtestadosMessageSender atestadosMessageSender, IReceitasMessageSender receitasMessageSender,
                                IConnectionMultiplexer redis, IMapper mapper)
    {
        _service = service;
        _logger = logger;
        _atestadosMessageSender = atestadosMessageSender;
        _receitasMessageSender = receitasMessageSender;
        _redis = redis;
        _mapper = mapper;
    }

    [Authorize(Policy = "DentistasEnfermeiros")]
    [HttpPost("atestado")]
    public async Task<ActionResult> GerarAtestado(AtestadoDTO atestado)
    {
        try
        {
            var claims = HttpContext.User.Claims;

            string usuario = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            Random random = new Random();

            int idAtestado = random.Next(10, 100);

            Atestado corpoAtestado = _mapper.Map<Atestado>(atestado);

            corpoAtestado.Usuario = usuario;
            corpoAtestado.Id = idAtestado;
            corpoAtestado.MessageCreated = DateTime.Now;

            await _atestadosMessageSender.SendMessage(corpoAtestado, "odonto.documentos.atestado");

            _logger.LogTrace("Atestado solicitado!");

            var retornoAtestado = new
            {
                id_atestado = idAtestado,
                status = "Atestado em processamento"
            };

            return Ok(retornoAtestado);
        }
        catch (CustomException ex)
        {
            _logger.LogError($" Erro: {ex.StatusCode} {ex.Message}");
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }

    [Authorize(Policy = "DentistasEnfermeiros")]
    [HttpPost("receita")]
    public async Task<ActionResult> GerarReceita(ReceitaDTO receita)
    {
        try
        {
            var claims = HttpContext.User.Claims;

            string usuario = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var random = new Random();

            int idReceita = random.Next(10, 100);

            var corpoReceita = _mapper.Map<Receita>(receita);

            corpoReceita.Usuario = usuario;
            corpoReceita.Id = idReceita;
            corpoReceita.MessageCreated = DateTime.Now;

            await _receitasMessageSender.SendMessage(corpoReceita, "odonto.documentos.receita");

            _logger.LogTrace("Atestado solicitado!");

            var retornoAtestado = new
            {
                id_atestado = idReceita,
                status = "Atestado em processamento"
            };

            return Ok(retornoAtestado);
        }
        catch (CustomException ex)
        {
            _logger.LogError($" Erro: {ex.StatusCode} {ex.Message}");
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }

    [Authorize(Policy = "DentistasEnfermeiros")]
    [HttpGet("status-atestado/{id}")]
    public async Task<ActionResult> BuscarStatusDocumento(string id)
    {
        try
        {
            var redisDb = _redis.GetDatabase();

            // Recuperando o status do Redis
            var statusDocumento = redisDb.StringGet($"atestado:{id}:status");

            // Se o status não for encontrado
            if (!statusDocumento.HasValue)
            {
                return NotFound(new { message = "Status do atestado não encontrado." });
            }

            // Recuperando o resultado (Base64) do Redis
            var resultadoBase64 = redisDb.StringGet($"atestado:{id}:result");

            // Se o resultado não for encontrado
            if (!resultadoBase64.HasValue)
            {
                return NotFound(new { message = "Resultado do atestado não encontrado." });
            }

            // Retornando os dados encontrados
            var atestado = new
            {
                Status = statusDocumento.ToString(),
                ResultadoBase64 = resultadoBase64.ToString()
            };

            return Ok(atestado);

        }
        catch (CustomException ex)
        {
            _logger.LogError($"Erro: {ex.StatusCode} {ex.Message}");
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }

    [Authorize(Policy = "DentistasEnfermeiros")]
    [HttpGet("status-receita/{id}")]
    public async Task<ActionResult> BuscarStatusReceita(string id)
    {
        try
        {
            var redisDb = _redis.GetDatabase();

            // Recuperando o status do Redis
            var statusDocumento = redisDb.StringGet($"receita:{id}:status");

            // Se o status não for encontrado
            if (!statusDocumento.HasValue)
            {
                return NotFound(new { message = "Status da receita não encontrado." });
            }

            // Recuperando o resultado (Base64) do Redis
            var resultadoBase64 = redisDb.StringGet($"receita:{id}:result");

            // Se o resultado não for encontrado
            if (!resultadoBase64.HasValue)
            {
                return NotFound(new { message = "Resultado da receita não encontrado." });
            }

            // Retornando os dados encontrados
            var atestado = new
            {
                Status = statusDocumento.ToString(),
                ResultadoBase64 = resultadoBase64.ToString()
            };

            return Ok(atestado);

        }
        catch (CustomException ex)
        {
            _logger.LogError($"Erro: {ex.StatusCode} {ex.Message}");
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }
    [Authorize(Policy = "DentistasEnfermeiros")]
    [HttpPost("receita-teste")]

    public async Task<ActionResult> envioTeste(ReceitaDTO receita)
    {
        try
        {
            var claims = HttpContext.User.Claims;

            string usuario = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var random = new Random();

            int idReceita = random.Next(10, 100);

            var corpoReceita = _mapper.Map<Receita>(receita);

            corpoReceita.Usuario = usuario;
            corpoReceita.Id = idReceita;
            corpoReceita.MessageCreated = DateTime.Now;
            
            await _service.GerarReceita(corpoReceita);
            
            return Ok("enviou");
        }
        catch (CustomException ex)
        {
            _logger.LogError($"Erro: {ex.StatusCode} {ex.Message}");
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }
}