namespace Odonto.Application.Interfaces;
using Odonto.Application.Documentos;


public interface IDocumentosService
{
    Task<byte[]> GerarReceita(Receita receita, string email);
    Task<byte[]> GerarAtestado(Atestado atestado, string email);
}
