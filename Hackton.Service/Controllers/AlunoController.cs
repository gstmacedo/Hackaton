using Hackton.Domain.Entities;
using Hackton.Domain.ModelsFromView;
using Hackton.Service.Service;
using Microsoft.AspNetCore.Mvc;

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
    }
}
