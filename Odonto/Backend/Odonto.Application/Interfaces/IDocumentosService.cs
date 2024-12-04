namespace Odonto.Application.Interfaces;

using Odonto.API.DTOs.Documentos;


public interface IDocumentosService
{
    Task<byte[]> GerarReceita(Receita receita);
    Task<byte[]> GerarAtestado(Atestado atestado);
}
