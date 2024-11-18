namespace Odonto.Application.Interfaces;

using Odonto.API.DTOs.Documentos;
using Odonto.Application.Documentos;


public interface IDocumentosService
{
    Task<byte[]> GerarReceita(ReceitaDTO receita, string email);
    Task<byte[]> GerarAtestado(AtestadoDTO atestado, string email);
}
