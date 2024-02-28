using Core.DTO.request;
using Core.DTO.response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace agremiacion.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class CobranzaController(ICobranzaService cobranza) : ControllerBase
    {
        private readonly ICobranzaService _service = cobranza;

        [HttpGet]
        public async Task<IEnumerable<CobranzaDtoOut>> Get()
        {
            return await _service.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CobranzaDtoOut>>? GetById(int id)
        {
            var cobranza = await _service.GetDtoById(id);

            if (cobranza is null)
                return NotFound(id);

            return cobranza;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CobranzaDtoIn cobranza)
        {
            //creando un nuevo objeto
            var newCobranza = await _service.Create(cobranza);

            return CreatedAtAction(nameof(GetById), new { id = newCobranza.Id }, newCobranza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CobranzaDtoIn cobranza)
        {
            if (id != cobranza.Id)
                return BadRequest();

            var cobranzaToUpdate = await _service.GetById(id);

            if (cobranzaToUpdate is not null)
            {
                await _service.Update(id, cobranza);
                return NoContent();
            }
            else
            { return NotFound(); }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cobranzaToDelete = await _service.GetById(id);

            if (cobranzaToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            { return NotFound(); }
        }

    }
}
