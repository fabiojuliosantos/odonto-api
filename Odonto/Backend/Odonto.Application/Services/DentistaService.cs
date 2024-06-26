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
        throw new NotImplementedException();
    }

    public Task<Dentista> BuscarPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Dentista>> BuscarTodosDentistasAsync()
    {
        throw new NotImplementedException();
    }

    public Dentista CadastrarDentista(Dentista dentista)
    {
        throw new NotImplementedException();
    }

    public Dentista ExcluirDentista(Dentista dentista)
    {
        throw new NotImplementedException();
    }
}
