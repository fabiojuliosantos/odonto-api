using AutoMapper;
using Odonto.API.DTOs.Consultas;
using Odonto.API.Models;

namespace Odonto.API.DTOs.Mappings;

public class OdontoDTOMappingProfile : Profile
{
    public OdontoDTOMappingProfile()
    {
        CreateMap<Consulta, ConsultasDTO>().ReverseMap();
        CreateMap<Consulta, ConsultasCadastroDTO>().ReverseMap();
    }
}