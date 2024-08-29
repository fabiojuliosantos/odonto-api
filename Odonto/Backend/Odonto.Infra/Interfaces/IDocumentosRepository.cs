using Odonto.Domain.Entities;

namespace Odonto.Infra.Interfaces;

public interface IDocumentosRepository
{
    Task<Documento> CadastrarDocumento(Documento documento);

}
