using Dapper;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;
using System.Data;

namespace Odonto.Infra.Repositories;

public class PacienteRepository : IPacienteRepository
{
    IDbConnection _connection;

    public PacienteRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    #region Cadastrar

    public async Task<Paciente> CadastrarNovo(Paciente paciente)
    {
        var sql = @"INSERT INTO PACIENTES(NOME,CPF,EMAIL,TELEFONE,CEP,LOGRADOURO,NUMEROCASA)
                           VALUES(@NOME,@CPF,@EMAIL,@TELEFONE,@CEP,@LOGRADOURO,@NUMEROCASA)";
        object parametros = new
        {
            NOME = paciente.Nome,
            CPF = paciente.Cpf,
            EMAIL = paciente.Email,
            TELEFONE = paciente.Telefone,
            CEP = paciente.Cep,
            LOGRADOURO = paciente.Logradouro,
            NUMEROCASA = paciente.NumeroCasa
        };

        var cadastrados = await _connection.ExecuteAsync(sql, parametros);
        if (cadastrados > 1) return paciente;
        return null;
    }

    #endregion Cadastrar

    #region Atualizar

    public async Task<Paciente> AtualizarPaciente(Paciente paciente)
    {
        var sql = @"UPDATE PACIENTES SET NOME=@NOME, CPF=@CPF, EMAIL=@EMAIL, 
                           TELEFONE=@TELEFONE, CEP=@CEP, LOGRADOURO=@LOGRADOURO, 
                           NUMEROCASA=@NUMEROCASA";

        object parametros = new
        {
            NOME = paciente.Nome,
            CPF = paciente.Cpf,
            EMAIL = paciente.Email,
            TELEFONE = paciente.Telefone,
            CEP = paciente.Cep,
            LOGRADOURO = paciente.Logradouro,
            NUMEROCASA = paciente.NumeroCasa
        };

        var atualizados = await _connection.ExecuteAsync(sql, parametros);
        if (atualizados > 1) return paciente;
        return null;
    }

    #endregion Atualizar

    #region Excluir

    public async Task<Paciente> ExcluirPaciente(int id)
    {
        var paciente = new Paciente();
        var sql = $"DELETE FROM PACIENTES WHERE PACIENTEID={id}";
        var excluidos = await _connection.ExecuteAsync(sql);
        if (excluidos > 1) return paciente;

        return paciente;
    }

    #endregion Excluir

    #region Buscar

    public async Task<IEnumerable<Paciente>> BuscarTodos()
    {
        var sql = "SELECT * FROM PACIENTES";

        var pacientes = await _connection.QueryAsync<Paciente>(sql);

        return pacientes.ToList();
    }

    public async Task<Paciente> BuscarPorId(int id)
    {
        var sql = $"SELECT * FROM PACIENTES WHERE PACIENTEID={id}";

        var paciente = await _connection.QueryFirstOrDefaultAsync<Paciente>(sql);

        return paciente;
    }

    #endregion Buscar
}