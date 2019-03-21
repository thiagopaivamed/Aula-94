using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Mapeamentos
{
    public class AdministradoresMap : IEntityTypeConfiguration<Administrador>
    {
        public void Configure(EntityTypeBuilder<Administrador> builder)
        {
            builder.HasKey(a => a.AdministradorId);

            builder.Property(a => a.Nome).IsRequired();
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Senha).IsRequired();

            builder.ToTable("Administradores");
        }
    }
}
