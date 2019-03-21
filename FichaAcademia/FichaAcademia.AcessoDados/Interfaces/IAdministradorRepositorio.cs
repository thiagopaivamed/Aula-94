using FichaAcademia.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Interfaces
{
    public interface IAdministradorRepositorio : IRepositorioGenerico<Administrador>
    {
        bool AdministradorExiste(string email, string senha);
    }
}
