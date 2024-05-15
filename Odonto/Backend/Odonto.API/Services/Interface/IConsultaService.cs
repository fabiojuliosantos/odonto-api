using Odonto.API.Models;

namespace Odonto.API.Services.Interface;

public interface IConsultaService
{
    IEnumerable<Consulta> BuscarTodasConsultas();
    Consulta BuscarConsultaPorId(int id);
    Consulta CadastrarConsulta(Consulta consulta);
    Consulta AtualizarConsulta(Consulta consulta);
    Consulta ExcluirConsulta(Consulta consulta);
}
