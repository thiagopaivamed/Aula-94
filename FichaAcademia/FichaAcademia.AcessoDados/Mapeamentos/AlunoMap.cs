using FichaAcademia.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FichaAcademia.AcessoDados.Mapeamentos
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(a => a.AlunoId);

            builder.Property(a => a.NomeCompleto).HasMaxLength(250).IsRequired();
            builder.Property(a => a.Idade).IsRequired();
            builder.Property(a => a.Peso).IsRequired();
            builder.Property(a => a.FrequenciaSemanal).IsRequired();

            builder.HasOne(a => a.Objetivo).WithMany(a => a.Alunos).HasForeignKey(a => a.ObjetivoId);
            builder.HasOne(a => a.Professor).WithMany(a => a.Alunos).HasForeignKey(a => a.ProfessorId);

            builder.HasMany(a => a.Fichas).WithOne(a => a.Aluno).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Alunos");
        }
    }
}
