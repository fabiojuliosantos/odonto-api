using Odonto.Application.Documentos;
using Odonto.Application.Interfaces;

namespace Odonto.Application.Services;

public class DocumentoService : IDocumentosService
{
    public Task<byte[]> GerarAtestado(Atestado atestado, string email)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> GerarReceita(Receita receita, string email)
    {
        throw new NotImplementedException();
    }
}
