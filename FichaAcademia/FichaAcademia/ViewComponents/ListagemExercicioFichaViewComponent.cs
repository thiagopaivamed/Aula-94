using FichaAcademia.AcessoDados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FichaAcademia.ViewComponents
{
    public class ListagemExercicioFichaViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public ListagemExercicioFichaViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int FichaId)
        {
            return View(await _contexto.ListasExercicios.Include(l => l.Exercicio).Where(l => l.FichaId == FichaId).ToListAsync());
        }
    }
}
