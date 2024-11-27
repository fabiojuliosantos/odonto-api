using Dapper;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;
using System.Data;

namespace Odonto.Infra.Repositories;

public class DocumentosRepository : IDocumentosRepository
{
    private readonly IDbConnection _connection;

    public DocumentosRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Documento> CadastrarDocumento(Documento documento)
    {
        try
        {
            string sql = @"INSERT INTO DOCUMENTOS(TIPODOCUMENTO, DATAEMISSAO, DENTISTAID, PACIENTEID, NOMEDOCUMENTO)
                           VALUES(@TIPODOCUMENTO, @DATAEMISSAO, @DENTISTAID, @PACIENTEID, @NOMEARQUIVO)";

            object parametros = new
            {
                TIPODOCUMENTO = documento.TipoDocumento,
                DATAEMISSAO = documento.DataEmissao,
                DENTISTAID = documento.DentistaId,
                PACIENTEID = documento.PacienteId,
                NOMEARQUIVO = documento.NomeDocumento
            };

            int cadastrados = await _connection.ExecuteAsync(sql, parametros);

            if (cadastrados > 0) return documento;
            return null;
        }
        catch
        {
            throw;
        }
    }
}
