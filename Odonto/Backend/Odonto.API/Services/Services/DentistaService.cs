using Odonto.API.Models;
using Odonto.API.Repositories.Interface;
using Odonto.API.Services.Interface;

namespace Odonto.API.Services.Services;

public class DentistaService : IDentistaService
{
    IDentistaRepository _repository;
    public DentistaService(IDentistaRepository repository)
    {
        _repository = repository;
    }

    #region Buscar
    public IEnumerable<Dentista> BuscarTodosDentistas()
    {
        return _repository.BuscarTodos();
    }
    public Dentista BuscarPorId(int id)
    {
        var dentista = _repository.BuscarDentistaPeloIdConsulta(id);
        if (dentista is null) throw new Exception($"Dentista de id: {id} não foi encontrado!");
        return dentista;

    }
    #endregion Buscar

    #region Cadastrar

    public Dentista CadastrarDentista(Dentista dentista)
    {
        if (dentista is null) throw new Exception("Não foram informados dados para o dentista!");

        _repository.Cadastrar(dentista);

        return dentista;
    }

    #endregion Cadastrar

    #region Atualizar

    public Dentista AtualizarDentista(Dentista dentista)
    {
        if (dentista is null) throw new Exception("Não foram informados dados para o dentista!");

        _repository.Atualizar(dentista);

        return dentista;
    }

    #endregion Atualizar

    #region Excluir

    public Dentista ExcluirDentista(Dentista dentista)
    {
        if (dentista is null) throw new Exception("Não foram informados dados para o dentista!");

        _repository.Deletar(dentista);

        return dentista;
    }

    #endregion Excluir
}
