﻿namespace Odonto.API.DTOs.Documentos;

public class ReceitaMensagem
{
    public int PacienteId { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
}
