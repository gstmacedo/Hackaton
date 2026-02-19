using Hackton.Domain;
using Hackton.Domain.DTO;
using Hackton.Domain.Entities;
using Hackton.Domain.ModelsFromView;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

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
