using Hackton.Domain.DTO;
using Hackton.Domain.Entities;
using Hackton.Domain.ModelsFromView;
using Hackton.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Hackton.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService _alunoService;
        private readonly IWebHostEnvironment _env;

        public AlunoController(AlunoService alunoService,
                               IWebHostEnvironment env)
        {
            _alunoService = alunoService;
            _env = env;
        }
        [HttpPost("finalizar-cadastro")]
        public async Task<IActionResult> EnviarCadastro(
            [FromForm] AlunoCompleto model,
            [FromForm] string token,
            IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Arquivo não enviado.");

            if (arquivo.Length > 5 * 1024 * 1024)
                return BadRequest("Arquivo muito grande. Máx 5MB.");

            var pastaUploads = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(pastaUploads))
                Directory.CreateDirectory(pastaUploads);

            var nomeArquivo = $"{Guid.NewGuid()}_{arquivo.FileName}";
            var caminhoCompleto = Path.Combine(pastaUploads, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            
            var resultado = await _alunoService
                .CriarAlunoCompletoAsync(model, token);

            if (!resultado)
                return BadRequest("Token inválido ou expirado.");

            return Ok("Cadastro realizado com sucesso.");
        }

        [HttpGet]
        public async Task<ActionResult<List<Aluno>>> GetAllAlunos()
        {
            var alunos = await _alunoService.GetAllAlunosAsync();
            return Ok(alunos);
        }

        [HttpGet("GetAlunoById")]
        public async Task<ActionResult<AlunoCompleto>> GetAlunoById(Guid id)
        {
            var alunoCompleto = await _alunoService.GetAlunoByIdAsync(id);
            if (alunoCompleto.Aluno == null)
            {
                return NotFound();
            }
            return Ok(alunoCompleto);
        }

        [HttpPost("ValidateStudent")]
        public async Task<ActionResult> ValidateStudent([FromBody] ValidateBody validate)
        {
            await _alunoService.ValidateStudentAsync(validate);
            return Ok();
        }

        [HttpPut("UpdateAluno")]
        public async Task<ActionResult> UpdateAluno([FromBody] AlunoCompleto aluno)
        {
            await _alunoService.UpdateAlunoAsync(aluno);
            return Ok();
        }

        [HttpDelete("DeleteAluno")]
        public async Task<ActionResult> DeleteAluno(Guid id)
        {
            await _alunoService.DeleteAlunoAsync(id);
            return Ok();


        }
    }
}
