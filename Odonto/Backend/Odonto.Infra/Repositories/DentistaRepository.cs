using Dapper;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;
using System.Data;

namespace Odonto.Infra.Repositories;

public class DentistaRepository : IDentistaRepository
{
    private readonly IDbConnection _connection;

    public DentistaRepository(IDbConnection connection)
    {
        _connection = connection;
    }


    #region Atualizar

    public async Task<Dentista> AtualizarDentista(Dentista dentista)
    {
        var sql = @"UPDATE DENTISTAS SET NOME=@NOME, EMAIL=@EMAIL, TELEFONE=@TELEFONE, CRO=@CRO WHERE DENTISTAID=@ID";

        object parametros = new
        {
            NOME = dentista.Nome,
            EMAIL = dentista.Email,
            TELEFONE = dentista.Telefone,
            CRO = dentista.Cro,
            ID = dentista.DentistaId
        };

        if (await _connection.ExecuteAsync(sql, parametros) > 1) return dentista;

        return null;
    }

    #endregion Atualizar

    #region Cadastrar

    public async Task<Dentista> CadastrarNovo(Dentista dentista)
    {
        var sql = @"INSERT INTO DENTISTAS(NOME, EMAIL, TELEFONE, CRO)
                                  VALUES(@NOME, @EMAIL, @TELEFONE, @CRO)";
        object parametros = new
        {
            NOME = dentista.Nome,
            EMAIL = dentista.Email,
            TELEFONE = dentista.Telefone,
            CRO = dentista.Cro
        };

        if (await _connection.ExecuteAsync(sql, parametros) > 1) return dentista;
        return null;
    }

    #endregion Cadastrar

    #region Excluir

    public async Task<Dentista> ExcluirDentista(int id)
    {
        var dentista = new Dentista();
        var sql = $"DELETE FROM DENTISTAS WHERE DENTISTAID = {id}";

        await _connection.ExecuteAsync(sql);

        return dentista;
    }

    #endregion Excluir

    #region Buscar

    public async Task<Dentista> BuscarPorId(int id)
    {
        var sql = $"SELECT * FROM DENTISTAS WHERE DENTISTAID={id}";

        var dentista = await _connection.QueryFirstOrDefaultAsync<Dentista>(sql);

        return dentista;
    }

    public async Task<IEnumerable<Dentista>> BuscarTodos()
    {
        var sql = "SELECT * FROM DENTISTAS";

        var dentistas = await _connection.QueryAsync<Dentista>(sql);

        return dentistas.ToList();
    }

    public async Task<Dentista> BuscarPorEmail(string email)
    {
        try
        {
            string sql = $"SELECT * FROM DENTISTAS WHERE EMAIL='{email}'";

            Dentista dentista = await _connection.QueryFirstOrDefaultAsync<Dentista>(sql);

            return dentista;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion Buscar
}