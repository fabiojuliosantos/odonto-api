using MediatR;
using Odonto.Application.Mediator.Documentos.Commands;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Mediator.Documentos.Handlers;

public class CadastrarDocumentosHandler : IRequestHandler<CadastrarDocumentosCommand, Documento>
{
    private readonly IDocumentosRepository _repository;

    public CadastrarDocumentosHandler(IDocumentosRepository repository)
    {
        _repository = repository;
    }

    public async Task<Documento> Handle(CadastrarDocumentosCommand request, CancellationToken cancellationToken)
    {
        Documento documentoCadastrar = new Documento()
        {
            TipoDocumento = request.TipoDocumento,
            DataEmissao = request.DataEmissao,
            DentistaId = request.DentistaId,
            PacienteId = request.PacienteId,
            NomeDocumento = request.NomeArquivo
        };

        await _repository.CadastrarDocumento(documentoCadastrar);
        return documentoCadastrar;
    }
}
