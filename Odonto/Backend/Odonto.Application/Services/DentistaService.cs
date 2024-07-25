using Odonto.Application.Interfaces;
using Odonto.Domain.Entities;
using Odonto.Infra.Interfaces;

namespace Odonto.Application.Services;

public class DentistaService : IDentistaService
{
    private readonly IDentistaRepository _repository;
    public DentistaService(IDentistaRepository repository)
    {
        _repository = repository;
    }
    public Dentista AtualizarDentista(Dentista dentista)
    {
        var dentistaExistente = _repository.BuscarDentistaPeloIdConsultaAsync(dentista.DentistaId);
        
        if (dentistaExistente is null) throw new Exception("Dentista não existente!");

        _repository.Atualizar(dentista);

        return dentista;
        
    }

    public async Task<Dentista> BuscarPorIdAsync(int id)
    {
        Dentista dentista = await _repository.BuscarDentistaPeloIdConsultaAsync(id);

        return dentista;
        
    }

    public async Task<IEnumerable<Dentista>> BuscarTodosDentistasAsync()
    {
        IEnumerable<Dentista> dentistas = await _repository.BuscarTodosAsync();

        return dentistas;
    }

    public Dentista CadastrarDentista(Dentista dentista)
    {
        if (dentista is null) throw new Exception("Não foram informados dados para cadastrar o dentista!");
        _repository.Cadastrar(dentista);
        return dentista;
    }

    public Dentista ExcluirDentista(Dentista dentista)
    {
        if (dentista is null) throw new Exception("Não foram informados dados para excluir o dentista!");
        _repository.Deletar(dentista);
        return dentista;
    }
}
