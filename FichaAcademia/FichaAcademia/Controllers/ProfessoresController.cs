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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace FichaAcademia.Controllers
{
    [Authorize]
    public class ProfessoresController : Controller
    {
        private readonly IProfessorRepositorio _professorRepositorio;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProfessoresController(IProfessorRepositorio professorRepositorio, IHostingEnvironment hostingEnvironment)
        {
            _professorRepositorio = professorRepositorio;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Professores
        public async Task<IActionResult> Index()
        {
            return View(await _professorRepositorio.PegarTodos().ToListAsync());
        } 
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessorId,Nome,Foto,Telefone,Turno")] Professor professor, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                if(arquivo != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        await arquivo.CopyToAsync(fileStream);
                        professor.Foto = "~/Imagens/" + arquivo.FileName;
                    }                   
                }

                await _professorRepositorio.Inserir(professor);
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }
       
        public async Task<IActionResult> Edit(int id)
        {
            var professor = await _professorRepositorio.PegarPeloId(id);
            if (professor == null)
            {
                return NotFound();
            }

            TempData["Professor"] = professor.Foto;

            return View(professor);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessorId,Nome,Foto,Telefone,Turno")] Professor professor, IFormFile arquivo)
        {
            if (id != professor.ProfessorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                if (arquivo != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        await arquivo.CopyToAsync(fileStream);
                        professor.Foto = "~/Imagens/" + arquivo.FileName;
                    }
                }

                else professor.Foto = TempData["Professor"].ToString();

                await _professorRepositorio.Atualizar(professor);

                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }  
       
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _professorRepositorio.Excluir(id);
            return Json("Professor excluído com sucesso");
        }

        public async Task<JsonResult> ProfessorExiste(string Nome, int ProfessorId)
        {
            if (ProfessorId == 0)
            {
                if(await _professorRepositorio.ProfessorExiste(Nome))
                    return Json("Professor já existe");

                return Json(true);
            }

            else
            {
                if (await _professorRepositorio.ProfessorExiste(Nome, ProfessorId))
                    return Json("Professor já existe");

                return Json(true);
            }
        }
    }
}
