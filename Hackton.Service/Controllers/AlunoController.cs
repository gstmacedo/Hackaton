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
        public AlunoController(AlunoService alunoService)
        {
            _alunoService = alunoService;
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
