using Odonto.API.Models;
using Odonto.API.Repositories.Interface;
using Odonto.API.Services.Interface;

namespace Odonto.API.Services.Services;

public class ConsultaService : IConsultaService
{
    IConsultaRepository _repository;
    public ConsultaService(IConsultaRepository repository)
    {
        _repository = repository;
    }

    #region Buscar
    public IEnumerable<Consulta> BuscarTodasConsultas()
    {
        var consultas = _repository.BuscarTodos();

        return consultas;
    }

    public Consulta BuscarConsultaPorId(int id)
    {
        if (id <= 0 || string.IsNullOrEmpty(id.ToString())) throw new Exception("Valor informado para id é inválido!");

        var consulta = _repository.BuscarConsultaComPacienteDentistaPorId(id);

        if (consulta is null) throw new Exception($"Consulta de id: {id} não encontrada!");

        return consulta;
    }

    #endregion Buscar

    #region Cadastrar
    public Consulta CadastrarConsulta(Consulta consulta)
    {
        if (consulta is null) throw new Exception("Dados para consulta não foram informados!");

        _repository.Cadastrar(consulta);

        return consulta;
    }

    #endregion Cadastrar

    #region Atualizar    
    public Consulta AtualizarConsulta(Consulta consulta)
    {
        if (consulta is null) throw new Exception("Dados para consulta não foram informados!");

        _repository.Atualizar(consulta);

        return consulta;
    }
    #endregion Atualizar

    #region Excluir
    public Consulta ExcluirConsulta(Consulta consulta)
    {
        if (consulta is null) throw new Exception("Dados para consulta não foram informados!");

        _repository.Deletar(consulta);

        return consulta;
    }
    #endregion Excluir
}
