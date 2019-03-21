using FichaAcademia.AcessoDados.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FichaAcademia.ViewComponents
{
    public class DadosAlunoViewComponent : ViewComponent
    {
        private readonly IAlunoRepositorio _alunoRepositorio;

        public DadosAlunoViewComponent(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync(int AlunoId)
        {
            return View(await _alunoRepositorio.PegarDadosAlunoPeloId(AlunoId));
        }
    }
}
