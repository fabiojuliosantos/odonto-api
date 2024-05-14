﻿using Odonto.API.Context;
using Odonto.API.Models;
using Odonto.API.Repositories.Interface;

namespace Odonto.API.Repositories.Repository;

public class PacienteRepository : Repository<Paciente>, IPacienteRepository
{
    public PacienteRepository(AppDbContext context) : base(context)
    {
    }

}