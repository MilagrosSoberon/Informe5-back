using Core.DTO.response;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.DTO.request;

namespace agremiacion.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EstadoPagoController(IEstadoPagoService estadoPago) : ControllerBase
    {
        private readonly IEstadoPagoService _service = estadoPago;

        [HttpGet]
        public async Task<IEnumerable<EstadoPagoDtoOut>> Get()
        {
            return await _service.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoPagoDtoOut>>? GetById(int id)
        {
            var estadoPago = await _service.GetDtoById(id);

            if (estadoPago is null)
                return NotFound(id);

            return estadoPago;
        }
        [HttpPost]
        public async Task<IActionResult> Create(EstadoPagoDtoIn estadopago)
        {
            //creando un nuevo objeto
            var newEstadoPago = await _service.Create(estadopago);

            return CreatedAtAction(nameof(GetById), new { id = newEstadoPago.Id }, newEstadoPago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EstadoPagoDtoIn estadopago)
        {
            if (id != estadopago.Id)
                return BadRequest();

            var estadoPagoToUpdate = await _service.GetById(id);

            if (estadoPagoToUpdate is not null)
            {
                await _service.Update(id, estadopago);
                return NoContent();
            }
            else
            { return NotFound(); }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var estadoPagoToDelete = await _service.GetById(id);

            if (estadoPagoToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            { return NotFound(); }
        }

    }
}
