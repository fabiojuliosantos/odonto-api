using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odonto.API.DTOs.Documentos;
using Odonto.Application.Interfaces;
using Odonto.Application.TratarErros;
using System.Security.Claims;

namespace Odonto.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentosController : ControllerBase
{
    private readonly IDocumentosService _service;
    private readonly ILogger<DocumentosController> _logger;
    public DocumentosController(IDocumentosService service, ILogger<DocumentosController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [Authorize]
    [HttpPost("gerar-atestado")]
    public async Task<ActionResult> GerarAtestado(AtestadoDTO atestado)
    {
        try
        {
            var claims = HttpContext.User.Claims;

            string usuario = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            byte[] atestadoDocumento = await _service.GerarAtestado(atestado, usuario);

            _logger.LogTrace("Atestado gerado!");
            return Ok(atestadoDocumento);
        }
        catch (CustomException ex)
        {
            _logger.LogError($" Erro: {ex.StatusCode} {ex.Message}");
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }

    [Authorize]
    [HttpPost("gerar-receita")]
    public async Task<ActionResult> GerarReceita(ReceitaDTO receita)
    {
        try
        {
            var claims = HttpContext.User.Claims;

            string usuario = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            byte[] receitaDocumento = await _service.GerarReceita(receita, usuario);

            _logger.LogTrace("Atestado gerado!");
            return Ok(receitaDocumento);
        }
        catch (CustomException ex)
        {
            _logger.LogError($" Erro: {ex.StatusCode} {ex.Message}");
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
    }
}