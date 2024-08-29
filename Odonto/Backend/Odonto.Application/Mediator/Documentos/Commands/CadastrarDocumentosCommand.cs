using MediatR;
using Odonto.Domain.Entities;

namespace Odonto.Application.Mediator.Documentos.Commands;

public class CadastrarDocumentosCommand : IRequest<Documento>
{
    public string TipoDocumento { get; set; }
    public DateTime DataEmissao { get; set; }
    public int DentistaId { get; set; }
    public int PacienteId { get; set; }
    public string NomeArquivo { get; set; }
}
