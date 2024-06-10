﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odonto.API.Models;

public class Paciente
{
    public Paciente()
    {
        Consultas = new Collection<Consulta>();
    }

    public int PacienteId { get; set; }
    
    #region Informacoes Basicas
    [Required] [StringLength(100)] public string Nome { get; set; }
    [Required] [Column(TypeName = "Date")] public DateTime DataNascimento { get; set; }
    #endregion Informacoes Basicas
    
    #region Contato
    [StringLength(80)] public string Email { get; set; }
    [StringLength(11)] public string Telefone { get; set; }
    #endregion Contato
    
    #region Endereco
    [StringLength(8)]public string Cep { get; set; }
    [StringLength(110)]public string Logradouro { get; set; }
    [StringLength(110)]public string Bairro { get; set; }
    public int NumeroCasa { get; set; }
    #endregion Endereco
    
    public ICollection<Consulta>? Consultas { get; set; }
}