using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hackton.Domain.Entities
{
    [Table("documentos_aluno")]
    public class DocumentoAluno
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("alunos")]
        [Column("aluno_id")]
        public Guid AlunoId { get; set; }

        public Aluno Aluno { get; set; }

        [Column("tipo")]
        public string TipoDocumento { get; set; }

        [Column("caminho_arquivo", TypeName = "TEXT")]
        public string CaminhoArquivo { get; set; }

        [Column("tipo_mime", TypeName = "varchar(100)")]
        public string TipoMime { get; set; }

        [Column("enviado_em", TypeName = "timestamp")]
        public DateTime EnviadoEm { get; set; }



    }
}
