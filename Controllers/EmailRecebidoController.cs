using LocamailApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocamailApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailRecebidoController : ControllerBase
    {
        private readonly IEmailRecebidoService _service;

        public EmailRecebidoController(IEmailRecebidoService service)
        {
            _service = service;
        }

        // GET: api/emailrecebido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailRecebidoModel>>> GetAll()
        {
            var emails = await _service.GetAllAsync();
            return Ok(emails);
        }

        // GET: api/emailrecebido/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmailRecebidoModel>> GetById(string id)
        {
            var email = await _service.GetByIdAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            return Ok(email);
        }

        // POST: api/emailrecebido
        [HttpPost]
        public async Task<ActionResult> Create(EmailRecebidoModel emailRecebidoModel)
        {
            await _service.CreateAsync(emailRecebidoModel);
            return CreatedAtAction(nameof(GetById), new { id = emailRecebidoModel.Id }, emailRecebidoModel);
        }

        // PUT: api/emailrecebido/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, EmailRecebidoModel emailRecebidoModel)
        {
            if (id != emailRecebidoModel.Id)
            {
                return BadRequest();
            }

            var existingEmail = await _service.GetByIdAsync(id);
            if (existingEmail == null)
            {
                return NotFound();
            }

            await _service.UpdateAsync(emailRecebidoModel);
            return NoContent();
        }

        // DELETE: api/emailrecebido/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existingEmail = await _service.GetByIdAsync(id);
            if (existingEmail == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
