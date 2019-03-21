using FichaAcademia.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FichaAcademia.AcessoDados.Interfaces
{
    public interface IAlunoRepositorio : IRepositorioGenerico<Aluno>
    {
        new Task<IEnumerable<Aluno>> PegarTodos();

        string PegarNomeAlunoPeloId(int id);

        Task<Aluno> PegarDadosAlunoPeloId(int AlunoId);

        Task<bool> AlunoExiste(string NomeCompleto);

        Task<bool> AlunoExiste(string NomeCompleto, int AlunoId);
    }
}
