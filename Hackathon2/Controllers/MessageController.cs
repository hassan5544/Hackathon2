using Hackathon2.Models;
using Hackathon2.Repositories;
using Hackathon2.Repositories.impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IMessageInterface _repository;

        public MessageController(IMessageInterface repository)
        {
            _repository = repository;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return Ok(await _repository.GetAllMessages());
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _repository.GetMessageById(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // POST: api/Messages
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            await _repository.AddMessage(message);
            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }

        // PUT: api/Messages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateMessage(message);

            return NoContent();
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _repository.GetMessageById(id);
            if (message == null)
            {
                return NotFound();
            }

            await _repository.DeleteMessage(id);

            return NoContent();
        }
    }

}
