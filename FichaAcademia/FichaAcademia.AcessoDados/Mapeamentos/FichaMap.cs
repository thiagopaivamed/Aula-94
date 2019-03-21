using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Mapeamentos
{
    public class FichaMap : IEntityTypeConfiguration<Ficha>
    {
        public void Configure(EntityTypeBuilder<Ficha> builder)
        {
            builder.HasKey(f => f.FichaId);

            builder.Property(f => f.Nome).HasMaxLength(50).IsRequired();
            builder.Property(f => f.Cadastro).IsRequired();
            builder.Property(f => f.Validade).IsRequired();

            builder.HasOne(f => f.Aluno).WithMany(f => f.Fichas).HasForeignKey(f => f.AlunoId);
            builder.HasMany(f => f.ListaExercicios).WithOne(f => f.Ficha).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Fichas");

        }
    }
}
