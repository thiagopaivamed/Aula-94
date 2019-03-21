using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FichaAcademia.AcessoDados.Interfaces;
using FichaAcademia.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FichaAcademia.Controllers
{
    public class AdministradoresController : Controller
    {
        private readonly IAdministradorRepositorio _administradorRepositorio;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdministradoresController(IAdministradorRepositorio administradorRepositorio, IHttpContextAccessor httpContextAccessor)
        {
            _administradorRepositorio = administradorRepositorio;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Clear();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdministradorViewModel administradorViewModel)
        {
            if(! _administradorRepositorio.AdministradorExiste(administradorViewModel.Email, administradorViewModel.Senha))
            {
                ModelState.AddModelError(string.Empty, "Email e/ou senhas inválidos");
                return View(administradorViewModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, administradorViewModel.Email)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");

            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);

            HttpContext.Session.SetString("Usuario", administradorViewModel.Email);

            ViewData["Usuario"] = HttpContext.Session.GetString("Usuario");

            return RedirectToAction("Index", "Alunos");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}