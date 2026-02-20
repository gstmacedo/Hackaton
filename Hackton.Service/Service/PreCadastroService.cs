using Hackton.Domain;
using Hackton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Hackton.Service.Service
{
    public class PreCadastroService
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public PreCadastroService(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task CriarPreCadastroAsync(PreCadastro preCadastro)
        {
            var token = Guid.NewGuid().ToString();

            var hash = GerarHash(token);

            preCadastro.HashToken = hash;
            preCadastro.ExpiraEm = DateTime.Now.AddDays(2);
            preCadastro.CriadoEm = DateTime.Now;

            _context.PreCadastros.Add(preCadastro);
            await _context.SaveChangesAsync();

            var link = $"https://localhost:7044/finalizar-cadastro/{token}";

            await _emailService.EnviarEmailAsync(preCadastro.Email, link);
        }

        private string GerarHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
        public async Task<PreCadastro?> ValidarTokenAsync(string token)
        {
            var hash = GerarHash(token);

            var preCadastro = await _context.PreCadastros
                .FirstOrDefaultAsync(p => p.HashToken == hash);

            if (preCadastro == null)
                return null;

            if (preCadastro.ExpiraEm < DateTime.UtcNow)
                return null;

            if (preCadastro.UtilizadoEm != default(DateTime))
                return null;

            return preCadastro;
        }
        public async Task MarcarComoUtilizadoAsync(PreCadastro preCadastro)
        {
            preCadastro.UtilizadoEm = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

    }
}
