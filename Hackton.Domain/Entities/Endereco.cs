using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Hackton.Domain.Entities
{
    [Table("enderecos")]
    public class Endereco
    {
        [Key]
        [Required]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("alunos")]
        [Column("aluno_id")]
        public int AlunoId { get; set; }

        public Aluno Aluno { get; set; }


        [Column("logradouro", TypeName = "varchar(255)")]
        public string Logradouro { get; set; }

        [Column("numero", TypeName = "varchar(20)")]
        public string Numero { get; set; }

        [Column("complemento", TypeName = "varchar(100)")]
        public string Complemento { get; set; }

        [Column("bairro", TypeName = "varchar(100)")]
        public string Bairro { get; set; }

        [Column("cidade", TypeName = "varchar(100)")]
        public string Cidade { get; set; }

        [Column("estado", TypeName = "varchar(2)")]
        public string Estado { get; set; }

        [Column("cep", TypeName = "varchar(10)")]
        public string Cep { get; set; }

        [Column("criado_em", TypeName = "timestamp")]
        public DateTime CriadoEm { get; set; }
    }
}
