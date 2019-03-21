using FichaAcademia.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FichaAcademia.AcessoDados.Interfaces
{
    public interface ICategoriaExercicioRepositorio : IRepositorioGenerico<CategoriaExercicio>
    {
        Task<bool> CategoriaExiste(string categoria);
        Task<bool> CategoriaExiste(string categoria, int CategoriaExercicioId);
    }
}
