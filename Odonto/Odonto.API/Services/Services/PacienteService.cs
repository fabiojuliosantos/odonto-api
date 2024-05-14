using Odonto.API.Exceptions;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;
using Odonto.API.Services.Interface;

namespace Odonto.API.Services.Services;

public class PacienteService : IPacienteService
{
    IPacienteRepository _repository;

    public PacienteService(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Paciente> BuscarTodosPacientes()
    {
        var pacientes = _repository.BuscarTodosPacientes();

        if (pacientes is null) throw new Exception("Não existem pacientes cadastrados!");

        return pacientes;
    }
    public Paciente BuscarPacientePorId(int id)
    {
        if (id <= 0 || string.IsNullOrEmpty(id.ToString())) throw new Exception("Valor de id inválido!");

        var paciente = _repository.BuscarPacientePorId(id);

        if (paciente is null) throw new Exception("Paciente informado não está cadastrado!");

        return paciente;
    }
    public Paciente AtualizarPaciente(Paciente paciente)
    {
        if (paciente is null) throw new Exception("Não foram fornecidos dados do paciente!");

        _repository.AtualizarPaciente(paciente);

        return paciente;
    }

    public Paciente CadastrarPaciente(Paciente paciente)
    {
        if (paciente is null) throw new Exception("Não foram fornecidos dados do paciente!");

        _repository.CadastrarPaciente(paciente);

        return paciente;
    }

    public Paciente ExcluirPaciente(int id)
    {
        if (id <= 0 || string.IsNullOrEmpty(id.ToString())) throw new Exception("Valor informado para o id é inválido!");

        var paciente = _repository.BuscarPacientePorId(id);

        if (paciente is null) throw new Exception("Paciente não encontrado!");
        
        _repository.ExcluirPaciente(paciente);
        
        return paciente;

    }
}
