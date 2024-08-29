using Dapper;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;
using System.Data;

namespace Odonto.Infra.Repositories;

public class ConsultaRepository : IConsultaRepository
{
    private readonly IDbConnection _connection;

    public ConsultaRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Consulta> AtualizarConsulta(Consulta consulta)
    {
        var sql = @"UPDATE CONSULTAS SET DESCRICAO=@DESCRICAO, DATACONSULTA=@DATACONSULTA, DENTISTAID=@DENTISTAID, PACIENTEID=@PACIENTEID";

        object parametros = new
        {
            DESCRICAO = consulta.Descricao,
            DATACONSULTA = consulta.DataConsulta,
            DENTISTAID = consulta.DentistaId,
            PACIENTEID = consulta.PacienteId
        };

        var atualizados = await _connection.ExecuteAsync(sql, parametros);

        if (atualizados > 0) return consulta;
        return null;
    }

    public async Task<IEnumerable<Consulta>> BuscarConsultaPorDentista(int dentistaId)
    {
        var sql = $"SELECT * FROM CONSULTAS WHERE DENTISTAID={dentistaId}";
        var consultas = await _connection.QueryAsync<Consulta>(sql);
        return consultas;
    }

    public async Task<Consulta> BuscarConsultaPorId(int id)
    {
        var sql = $"SELECT * FROM CONSULTAS WHERE CONSULTAID={id}";
        var consulta = await _connection.QueryFirstOrDefaultAsync<Consulta>(sql);
        return consulta;
    }

    public async Task<IEnumerable<Consulta>> BuscarConsultaPorPaciente(int pacienteId)
    {
        var sql = $"SELECT * FROM CONSULTAS WHERE PACIENTEID={pacienteId}";
        var consultas = await _connection.QueryAsync<Consulta>(sql);
        return consultas;
    }

    public async Task<IEnumerable<Consulta>> BuscarTodasConsultas()
    {
        var sql = "SELECT * FROM CONSULTAS";
        var consultas = await _connection.QueryAsync<Consulta>(sql);
        return consultas;
    }

    public async Task<Consulta> CadastrarConsulta(Consulta consulta)
    {
        var sql = @"INSERT INTO CONSULTAS(DESCRICAO,DATACONSULTA,DENTISTAID,PACIENTEID)
                           VALUES(@DESCRICAO,@DATACONSULTA,@DENTISTAID,@PACIENTEID)";

        object parametros = new
        {
            DESCRICAO = consulta.Descricao,
            DATACONSULTA = consulta.DataConsulta,
            DENTISTAID = consulta.DentistaId,
            PACIENTEID = consulta.PacienteId
        };

        var cadastro = await _connection.ExecuteAsync(sql, parametros);
        if (cadastro > 0) return consulta;
        return null;
    }

    public async Task<Consulta> ExcluirConsulta(int id)
    {
        var consulta = new Consulta();

        var sql = $"DELETE FROM CONSULTAS WHERE CONSULTAID={id}";

        var exclusao = await _connection.ExecuteAsync(sql);
        if (exclusao > 0) return consulta;
        return null;
    }
}