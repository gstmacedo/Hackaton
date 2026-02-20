using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hackton.Domain.Entities
{
    [Table("alunos")]
    public class Aluno
    {
        [Key]
        [Required]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        
        [Column("pre_cadastro_id")]
        public Guid PreCadastroId { get; set; }


        [Column("nome_completo", TypeName = "varchar(255)")]
        public string Nome { get; set; }

        [Column("nome_mae", TypeName = "varchar(255)")]
        public string NomeName { get; set; }

        [Column("nome_pai", TypeName = "varchar(255)")]
        public string NomePai { get; set; }

        [Column("rg_aluno", TypeName = "varchar(20)")]
        public string RgAluno { get; set; }

        [Column("rg_responsavel", TypeName = "varchar(20)")]
        public string RgResponsavel { get; set; }

        [Column("status", TypeName = "varchar(20)")]
        public string Status { get; set; }

        [Column("atualizado_em", TypeName = "timestamp")]
        public DateTime AtualizadoEm { get; set; }

        [Column("criado_em", TypeName = "timestamp")]
        public DateTime CriadoEm { get; set; }


        [Column("data_nascimento", TypeName = "timestamp")]
        public DateTime DataNascimento { get; set; }





    }
}
