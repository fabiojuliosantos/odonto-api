﻿namespace Odonto.Mvc.Models;

public class PacienteViewModel
{
    public int PacienteId { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
}
