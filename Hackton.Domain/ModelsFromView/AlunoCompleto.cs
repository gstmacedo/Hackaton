using Hackton.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackton.Domain.ModelsFromView
{
    public class AlunoCompleto
    {
        public Aluno? Aluno { get; set; }

        public Endereco? Endereco { get; set; }

        public List<DocumentoAluno>? DocumentosAluno { get; set; }
    }
}
