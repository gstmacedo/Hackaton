using Hackton.Domain;
using Hackton.Domain.Entities;
using Hackton.Domain.ModelsFromView;
using Microsoft.EntityFrameworkCore;

namespace Hackton.Service.Service
{
    public class AlunoService
    {
        private readonly AppDbContext _context;
        public AlunoService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Aluno>> GetAllAlunosAsync()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task<AlunoCompleto> GetAlunoByIdAsync(Guid id)
        {
            AlunoCompleto alunoCompleto = new AlunoCompleto();
            alunoCompleto.Aluno = await _context.Alunos.FindAsync(id);
            alunoCompleto.Endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.AlunoId == id);
            //alunoCompleto.DocumentoAluno = await _context.DocumentosAluno.FirstOrDefaultAsync(d => d.AlunoId == id);

            return alunoCompleto;

        }
    }
}
