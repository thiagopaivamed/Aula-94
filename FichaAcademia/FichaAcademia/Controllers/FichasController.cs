using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FichaAcademia.AcessoDados;
using FichaAcademia.Dominio.Models;
using FichaAcademia.AcessoDados.Interfaces;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Authorization;

namespace FichaAcademia.Controllers
{
    [Authorize]
    public class FichasController : Controller
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IFichaRepositorio _fichaRepositorio;

        public FichasController(IAlunoRepositorio alunoRepositorio, IFichaRepositorio fichaRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
            _fichaRepositorio = fichaRepositorio;
        }

        // GET: Fichas
        public async Task<IActionResult> Index(int AlunoId)
        {
            ViewBag.AlunoId = AlunoId;
            return View(await _fichaRepositorio.PegarTodasFichasPeloAlunoId(AlunoId));
        }

        // GET: Fichas/Details/5
        public async Task<IActionResult> Details(int FichaId)
        {
            var ficha = await _fichaRepositorio.PegarFichaAlunoPeloId(FichaId);
            if (ficha == null)
            {
                return NotFound();
            }

            return View(ficha);
        }

        public async Task<IActionResult> VisualizarPDF(int FichaId)
        {
            var ficha = await _fichaRepositorio.PegarFichaAlunoPeloId(FichaId);
            if (ficha == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("PDF", ficha) { FileName = "Ficha.PDF" };
        }

        public IActionResult Create(int AlunoId)
        {
            Ficha ficha = new Ficha();
            ficha.AlunoId = AlunoId;
            return View(ficha);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FichaId,Nome,Cadastro,Validade,AlunoId")] Ficha ficha)
        {
            ficha.Cadastro = DateTime.Parse(DateTime.Now.ToShortDateString());
            ficha.Validade = DateTime.Parse(DateTime.Now.AddYears(1).ToShortDateString());

            if (ModelState.IsValid)
            {
                await _fichaRepositorio.Inserir(ficha);
                return RedirectToAction(nameof(Index), new { AlunoId = ficha.AlunoId });
            }
            return View(ficha);
        }

        public async Task<IActionResult> Edit(int FichaId)
        {
            var ficha = await _fichaRepositorio.PegarPeloId(FichaId);
            if (ficha == null)
            {
                return NotFound();
            }

            return View(ficha);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int FichaId, [Bind("FichaId,Nome,Cadastro,Validade,AlunoId")] Ficha ficha)
        {
            if (FichaId != ficha.FichaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _fichaRepositorio.Atualizar(ficha);
                return RedirectToAction(nameof(Index), new { AlunoId = ficha.AlunoId });
            }

            return View(ficha);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int FichaId)
        {
            await _fichaRepositorio.Excluir(FichaId);
            return Json("Ficha excluída com sucesso");
        }

        public async Task<JsonResult> FichaExiste(string Nome, int FichaId)
        {
            if (FichaId == 0)
            {
                if (await _fichaRepositorio.FichaExiste(Nome))
                    return Json("Ficha já cadastrada");

                return Json(true);
            }
            else
            {
                if (await _fichaRepositorio.FichaExiste(Nome, FichaId))
                    return Json("Ficha já cadastrada");

                return Json(true);
            }
        }
    }
}
