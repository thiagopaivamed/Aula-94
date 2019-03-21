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
using Microsoft.AspNetCore.Authorization;

namespace FichaAcademia.Controllers
{
    [Authorize]
    public class ObjetivosController : Controller
    {
        private readonly IObjetivoRepositorio _objetivoRepositorio;

        public ObjetivosController(IObjetivoRepositorio objetivoRepositorio)
        {
            _objetivoRepositorio = objetivoRepositorio;
        }

        // GET: Objetivos
        public async Task<IActionResult> Index()
        {
            return View(await _objetivoRepositorio.PegarTodos().ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObjetivoId,Nome,Descricao")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                await _objetivoRepositorio.Inserir(objetivo);
                return RedirectToAction(nameof(Index));
            }
            return View(objetivo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objetivo = await _objetivoRepositorio.PegarPeloId(id);
            if (objetivo == null)
            {
                return NotFound();
            }
            return View(objetivo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObjetivoId,Nome,Descricao")] Objetivo objetivo)
        {
            if (id != objetivo.ObjetivoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _objetivoRepositorio.Atualizar(objetivo);
                return RedirectToAction(nameof(Index));
            }

            return View(objetivo);
        }

       
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _objetivoRepositorio.Excluir(id);
            return Json("Objetivo excluído com sucesso");
        }

       public async Task<JsonResult> ObjetivoExiste(string Nome, int ObjetivoId)
        {
            if(ObjetivoId == 0)
            {
                if (await _objetivoRepositorio.ObjetivoExiste(Nome))
                    return Json("Objetivo já existe");

                return Json(true);
            }

            else
            {
                if (await _objetivoRepositorio.ObjetivoExiste(Nome, ObjetivoId))
                    return Json("Objetivo já existe");

                return Json(true);
            }
        }
    }
}
