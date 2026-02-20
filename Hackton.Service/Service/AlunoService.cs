using Hackton.Domain;
using Hackton.Domain.DTO;
using Hackton.Domain.Entities;
using Hackton.Domain.ModelsFromView;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
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
            alunoCompleto.Endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.AlunoId == id);
            alunoCompleto.DocumentosAluno = await _context.DocumentosAluno.Where(d => d.AlunoId == id).ToListAsync();

            return alunoCompleto;

        }

        public async Task<bool> CriarAlunoCompletoAsync(AlunoCompleto model, string token)
        {
            if (model.Aluno == null ||
                model.Endereco == null
                /*model.DocumentoAluno == null*/)
                return false;

            var hash = GerarHash(token);

            var preCadastro = await _context.PreCadastros
                .FirstOrDefaultAsync(p => p.HashToken == hash);

            if (preCadastro == null)
                return false;

            if (preCadastro.ExpiraEm < DateTime.Now)
                return false;

            

            model.Aluno.Id = Guid.NewGuid();
            model.Endereco.Id = Guid.NewGuid();
            //model.DocumentoAluno.Id = Guid.NewGuid();

            model.Aluno.PreCadastroId = preCadastro.id;

            model.Endereco.AlunoId = model.Aluno.Id;
            model.Endereco.CriadoEm = DateTime.Now;

            //model.DocumentoAluno.AlunoId = model.Aluno.Id;
            //model.DocumentoAluno.EnviadoEm = DateTime.UtcNow;

            model.Aluno.Status = "pendente";
            model.Aluno.CriadoEm = DateTime.Now;
            model.Aluno.AtualizadoEm = DateTime.Now;
            model.Aluno.DataNascimento = Convert.ToDateTime(model.Aluno.DataNascimento);
            _context.Alunos.Add(model.Aluno);
            _context.Enderecos.Add(model.Endereco);
            //_context.DocumentosAluno.Add(model.DocumentoAluno);

            preCadastro.UtilizadoEm = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }


        private string GerarHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
        public async Task ValidateStudentAsync(ValidateBody validate)
        {
            var aluno = await _context.Alunos.FindAsync(validate.Id);
            if (aluno != null)
            {
                var status = validate.Action == "Approve" ? "aprovado" : "reprovado";
                aluno.Status = status;
                _context.Alunos.Update(aluno);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAlunoAsync(AlunoCompleto alunoCompleto)
        {
            var aluno = await _context.Alunos.FindAsync(alunoCompleto.Aluno.Id);
            if (aluno != null)
            {
                aluno.Nome = alunoCompleto.Aluno.Nome;
                aluno.Status = alunoCompleto.Aluno.Status;
                aluno.RgResponsavel = alunoCompleto.Aluno.RgResponsavel;
                _context.Alunos.Update(aluno);
                var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.AlunoId == aluno.Id);
                if (endereco != null)
                {
                    endereco.Logradouro = alunoCompleto.Endereco.Logradouro;
                    endereco.Numero = alunoCompleto.Endereco.Numero;
                    endereco.Bairro = alunoCompleto.Endereco.Bairro;
                    endereco.Cidade = alunoCompleto.Endereco.Cidade;
                    endereco.Estado = alunoCompleto.Endereco.Estado;
                    endereco.Cep = alunoCompleto.Endereco.Cep;
                    endereco.Complemento = alunoCompleto.Endereco.Complemento;
                    _context.Enderecos.Update(endereco);
                }
                //var documento = await _context.DocumentosAluno.FirstOrDefaultAsync(d => d.AlunoId == aluno.Id);
                //if (documento != null)
                //{
                //   documento.TipoDocumento = alunoCompleto.DocumentoAluno.TipoDocumento;
                //   documento.CaminhoArquivo = alunoCompleto.DocumentoAluno.CaminhoArquivo;
                //    _context.DocumentosAluno.Update(documento);

                //}
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAlunoAsync(Guid id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
                var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.AlunoId == id);
                if (endereco != null)
                {
                    _context.Enderecos.Remove(endereco);
                }
                var documentos = await _context.DocumentosAluno.Where(d => d.AlunoId == id).ToListAsync();
                if (documentos != null)
                {
                    _context.DocumentosAluno.RemoveRange(documentos);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
