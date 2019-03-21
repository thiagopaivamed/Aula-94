using FichaAcademia.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FichaAcademia.AcessoDados.Interfaces
{
    public interface IListaExercicioRepositorio : IRepositorioGenerico<ListaExercicio>
    {
        Task<bool> ExercicioExisteNaFicha(int exercicioId, int fichaId);
    }
}
