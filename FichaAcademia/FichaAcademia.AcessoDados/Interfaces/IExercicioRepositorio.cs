using FichaAcademia.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FichaAcademia.AcessoDados.Interfaces
{
    public interface IExercicioRepositorio : IRepositorioGenerico<Exercicio>
    {
        new Task<IEnumerable<Exercicio>> PegarTodos();
        Task<bool> ExercicioExiste(string nome);
        Task<bool> ExercicioExiste(string nome, int ExercicioId);
    }
}
