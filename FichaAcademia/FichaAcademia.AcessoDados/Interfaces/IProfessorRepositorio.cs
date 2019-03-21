using FichaAcademia.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FichaAcademia.AcessoDados.Interfaces
{
    public interface IProfessorRepositorio : IRepositorioGenerico<Professor>
    {
        Task<bool> ProfessorExiste(string Nome);
        Task<bool> ProfessorExiste(string Nome, int ProfessorId);
    }
}
