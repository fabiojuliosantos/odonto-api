﻿using Odonto.Application.Interfaces;
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

    #region Buscar

    public async Task<IEnumerable<Consulta>> BuscarTodasConsultasAsync()
    {
        var consultas = await _repository.BuscarTodosAsync();
        return consultas;
    }

    public async Task<IPagedList<Consulta>> BuscarConsultasPaginadas(ConsultasParameters param)
    {
        return await _repository.BuscarConsultasPaginadas(param);
    }

    public async Task<Consulta> BuscarConsultaPorIdAsync(int id)
    {
        if (id <= 0 || string.IsNullOrEmpty(id.ToString())) throw new Exception("Valor informado para id é inválido!");

        var consulta = await _repository.BuscarConsultaComPacienteDentistaPorIdAsync(id);

        if (consulta is null) throw new Exception($"Consulta de id: {id} não encontrada!");

        return consulta;
    }

    #endregion Buscar
}