using LocamailApp.Models;
using LocamailApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocamailApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailEnviadoController : ControllerBase
    {
        private readonly IEmailEnviadoService _emailEnviadoService;

        public EmailEnviadoController(IEmailEnviadoService emailEnviadoService)
        {
            _emailEnviadoService = emailEnviadoService;
        }

        // GET: api/emailenviado
        [HttpGet]
        public async Task<IEnumerable<EmailEnviadoModel>> Get()
        {
            return await _emailEnviadoService.GetAllAsync();
        }

        // GET: api/emailenviado/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmailEnviadoModel>> Get(string id)
        {
            var emailEnviado = await _emailEnviadoService.GetByIdAsync(id);

            if (emailEnviado == null)
            {
                return NotFound();
            }

            return emailEnviado;
        }

        // POST: api/emailenviado
        [HttpPost]
        public async Task<ActionResult> Create(EmailEnviadoModel emailEnviado)
        {
            await _emailEnviadoService.CreateAsync(emailEnviado);
            return CreatedAtAction(nameof(Get), new { id = emailEnviado.Id }, emailEnviado);
        }

        // PUT: api/emailenviado/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, EmailEnviadoModel emailEnviado)
        {
            if (id != emailEnviado.Id)
            {
                return BadRequest();
            }

            await _emailEnviadoService.UpdateAsync(emailEnviado);
            return NoContent();
        }

        // DELETE: api/emailenviado/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var emailEnviado = await _emailEnviadoService.GetByIdAsync(id);

            if (emailEnviado == null)
            {
                return NotFound();
            }

            await _emailEnviadoService.DeleteAsync(id);
            return NoContent();
        }

    }

}
