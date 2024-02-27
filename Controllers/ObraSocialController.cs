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
    public class ObraSocialController(IObraSocialService obraSocial) : ControllerBase
    {
        private readonly IObraSocialService _service = obraSocial;

        [HttpGet]
        public async Task<IEnumerable<ObraSocialDtoOut>> Get()
        {
            return await _service.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ObraSocialDtoOut>>? GetById(int id)
        {
            var obraSocial = await _service.GetDtoById(id);

            if (obraSocial is null)
                return NotFound(id);

            return obraSocial;
        }
        [HttpPost]
        public async Task<IActionResult> Create(ObraSocialDtoIn obrasocial)
        {
            //creando un nuevo objeto
            var newObraSocial = await _service.Create(obrasocial);

            return CreatedAtAction(nameof(GetById), new { id = newObraSocial.Id }, newObraSocial);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ObraSocialDtoIn obrasocial)
        {
            if (id != obrasocial.Id)
                return BadRequest();

            var obraSocialToUpdate = await _service.GetById(id);

            if (obraSocialToUpdate is not null)
            {
                await _service.Update(id, obrasocial);
                return NoContent();
            }
            else
            { return NotFound(); }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obraSocialToDelete = await _service.GetById(id);

            if (obraSocialToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            { return NotFound(); }
        }

    }
}
