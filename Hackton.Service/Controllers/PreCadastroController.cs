using Hackton.Domain.Entities;
using Hackton.Domain.ModelsFromView;
using Hackton.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace Hackton.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreCadastroController : ControllerBase
    {
        private readonly PreCadastroService _service;

        public PreCadastroController(PreCadastroService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PreCadastroRequest request)
        {
            var preCadastro = new PreCadastro
            {
                Email = request.Email
            };

            await _service.CriarPreCadastroAsync(preCadastro);
            return Ok("Pré-cadastro criado e email enviado.");
        }

        [HttpGet("ValidarToken")]
        public async Task<IActionResult> ValidarToken(string token)
        {
            var resultado = await _service.ValidarTokenAsync(token);

            if (resultado == null)
                return BadRequest("Token inválido ou expirado.");

            return Ok(resultado.id);
        }
    }
}