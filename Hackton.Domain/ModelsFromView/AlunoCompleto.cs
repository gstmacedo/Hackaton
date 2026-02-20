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

<<<<<<< HEAD
        public DocumentoAluno? DocumentoAluno { get; set; }
=======
        public List<DocumentoAluno>? DocumentosAluno { get; set; }
>>>>>>> 9aeaa401bce812b872376b7f315dcb3f5de2ed12
    }
}
