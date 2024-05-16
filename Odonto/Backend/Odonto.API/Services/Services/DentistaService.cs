using Odonto.API.Models;
using Odonto.API.Repositories.Interface;
using Odonto.API.Services.Interface;

namespace Odonto.API.Services.Services;

public class DentistaService : IDentistaService
{
    private readonly IDentistaRepository _repository;

    public DentistaService(IDentistaRepository repository)
    {
        _repository = repository;
    }

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

    #region Buscar

    public async Task<IEnumerable<Dentista>> BuscarTodosDentistasAsync()
    {
        return await _repository.BuscarTodosAsync();
    }

    public async Task<Dentista> BuscarPorIdAsync(int id)
    {
        var dentista = await _repository.BuscarDentistaPeloIdConsultaAsync(id);
        if (dentista is null) throw new Exception($"Dentista de id: {id} não foi encontrado!");
        return dentista;
    }

    #endregion Buscar
}