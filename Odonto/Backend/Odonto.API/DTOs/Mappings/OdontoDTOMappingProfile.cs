using AutoMapper;
using Odonto.API.DTOs.Consultas;
using Odonto.API.DTOs.Dentistas;
using Odonto.API.DTOs.Pacientes;
using Odonto.API.Models;

namespace Odonto.API.DTOs.Mappings;

public class OdontoDTOMappingProfile : Profile
{
    public OdontoDTOMappingProfile()
    {
        CreateMap<Consulta, ConsultasDTO>().ReverseMap();
        CreateMap<Consulta, ConsultasCadastroDTO>().ReverseMap();
        CreateMap<Paciente, PacientesDTO>().ReverseMap();
        CreateMap<Paciente, PacientesCadastroDTO>().ReverseMap();
        CreateMap<Dentista, DentistasDTO>().ReverseMap();
        CreateMap<Dentista, DentistasCadastroDTO>().ReverseMap();
    }
}