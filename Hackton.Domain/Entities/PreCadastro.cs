using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Hackton.Domain.Entities
{
    [Table("pre_cadastros")]
    public class PreCadastro
    {
        [Key]
        [Required]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("email", TypeName = "varchar(255)")]
        public string email { get; set; }

        [Column("hash_token", TypeName = "TEXT")]
        public string hashToken { get; set; }

        [Column("expira_em", TypeName = "timestamp")]
        public DateTime ExpiraEm { get; set; }

        [Column("utilizado_em",TypeName = "timestamp")]
        public DateTime UtilizadoEm { get; set; }

        [Column("criado_em", TypeName = "timestamp")]
        public DateTime CriadoEm { get; set; }

    }
}
