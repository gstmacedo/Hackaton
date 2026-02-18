using Hackton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Hackton.Domain
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }
        public DbSet<PreCadastro> PreCadastros { get; set; }

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet <Endereco> Enderecos { get; set; }

        public DbSet <DocumentoAluno> DocumentosAluno { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }
        }
    }
}
