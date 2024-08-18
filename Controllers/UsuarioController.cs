using Microsoft.AspNetCore.Mvc;
using LocamailApp.Models;
using LocamailApp.Data.Repository;
using LocamailApp.Services;
using System.Reflection;
using LocamailApp.Exceptions;
using static MongoDB.Driver.WriteConcern;

namespace LocamailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IPasswordService _passwordService;


        // Atualize o construtor para injetar ambos os serviços
        public UsuarioController(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService, IPasswordService passwordService)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> Get()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Ok(usuarios);
        }

        // GET api/usuario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> Get(string id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // POST api/usuario
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioModel modelModel)
        {
            if (modelModel == null)
            {
                return BadRequest("Modelo de usuário não pode ser nulo.");
            }

            try
            {
                await _usuarioService.CreateUserAsync(modelModel.Nome, modelModel.Email, modelModel.Senha, modelModel.EmailRecuperacao);
                return Ok();
            }
            catch (UsuarioJaExisteException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // PUT api/usuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UsuarioModel usuarioModel)
        {
            if (id != usuarioModel.Id)
            {
                return BadRequest();
            }

            var existingUser = await _usuarioRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _usuarioRepository.UpdateAsync(usuarioModel);
            return NoContent();
        }

        // DELETE api/usuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingUser = await _usuarioRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _usuarioRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("alterar-senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlteracaoSenhaModel alteracaoSenhaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _usuarioService.AlterarSenha(alteracaoSenhaModel);
                return Ok("Senha alterada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/atualizar-email-recuperacao")]
        public async Task<IActionResult> AtualizarEmailRecuperacao(string id, [FromBody] string novoEmailRecuperacao)
        {
            if (string.IsNullOrWhiteSpace(novoEmailRecuperacao))
            {
                return BadRequest("O e-mail recuperação não pode estar em branco.");
            }

            try
            {
                await _usuarioService.AtualizarEmailRecuperacaoAsync(id, novoEmailRecuperacao);
                return Ok("E-mail recuperação atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
