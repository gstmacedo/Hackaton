using Hackton.Domain;
using Hackton.Domain.Entities;
using Hackton.Domain.ModelsFromView;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
            alunoCompleto.Endereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.AlunoId == id);

            alunoCompleto.DocumentoAluno = await _context.DocumentosAluno
                .FirstOrDefaultAsync(d => d.AlunoId == id);

            return alunoCompleto;
        }

        public async Task<bool> CriarAlunoCompletoAsync(AlunoCompleto model, string token)
        {
            if (model.Aluno == null ||
                model.Endereco == null ||
                model.DocumentoAluno == null)
                return false;

            var hash = GerarHash(token);

            var preCadastro = await _context.PreCadastros
                .FirstOrDefaultAsync(p => p.HashToken == hash);

            if (preCadastro == null)
                return false;

            if (preCadastro.ExpiraEm < DateTime.UtcNow)
                return false;

            if (preCadastro.UtilizadoEm != null)
                return false;

            model.Aluno.Id = Guid.NewGuid();
            model.Endereco.Id = Guid.NewGuid();
            model.DocumentoAluno.Id = Guid.NewGuid();

            model.Aluno.PreCadastroId = preCadastro.id;

            model.Endereco.AlunoId = model.Aluno.Id;
            model.Endereco.CriadoEm = DateTime.UtcNow;

            model.DocumentoAluno.AlunoId = model.Aluno.Id;
            model.DocumentoAluno.EnviadoEm = DateTime.UtcNow;

            model.Aluno.Status = "PENDENTE";
            model.Aluno.CriadoEm = DateTime.UtcNow;
            model.Aluno.AtualizadoEm = DateTime.UtcNow;

            _context.Alunos.Add(model.Aluno);
            _context.Enderecos.Add(model.Endereco);
            _context.DocumentosAluno.Add(model.DocumentoAluno);

            preCadastro.UtilizadoEm = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }


        private string GerarHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
