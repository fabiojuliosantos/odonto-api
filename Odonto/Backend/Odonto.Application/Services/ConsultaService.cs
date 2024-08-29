using Odonto.Application.Interfaces;
using Odonto.Domain.Entities;
using Odonto.Domain.Pagination;
using Odonto.Infra.Interfaces;
using X.PagedList;

namespace Odonto.Application.Services;

public class ConsultaService : IConsultaService
{
    private readonly IConsultaRepository _repository;

    public ConsultaService(IConsultaRepository repository)
    {
        _repository = repository;
    }

    #region Cadastrar

    public async Task<Consulta> CadastrarConsulta(Consulta consulta)
    {
        if (consulta is null) throw new Exception("Dados para consulta não foram informados!");

        await _repository.CadastrarConsulta(consulta);

        return consulta;
    }

    #endregion Cadastrar

    #region Atualizar

    public async Task<Consulta> AtualizarConsulta(Consulta consulta)
    {
        try
        {
            if (consulta is null) throw new Exception("Dados para consulta não foram informados!");

            await _repository.AtualizarConsulta(consulta);

            return consulta;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion Atualizar

    #region Excluir

    public async Task<Consulta> ExcluirConsulta(int id)
    {
        if (id < 1) throw new Exception("Dados para consulta não foram informados!");

        Consulta consulta = await BuscarConsultaPorIdAsync(id);

        await _repository.ExcluirConsulta(id);

        return consulta;
    }

    #endregion Excluir

    #region Buscar

    public async Task<IEnumerable<Consulta>> BuscarTodasConsultasAsync()
    {
        var consultas = await _repository.BuscarTodasConsultas();
        return consultas;
    }

    public async Task<Consulta> BuscarConsultaPorIdAsync(int id)
    {

        var consulta = await _repository.BuscarConsultaPorId(id);

        return consulta;
    }

    #endregion Buscar
}
