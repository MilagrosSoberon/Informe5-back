using Core.DTO.request;
using Core.DTO.response;
using Services;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace agremiacion.Controllers

{
    [ApiController]
    [Route("[controller]")]


    public class ObraSocialController : ControllerBase
    {
        private readonly IObraSocialService _service;

        public ObraSocialController(IObraSocialService obraSocial)
        {
            _service = obraSocial;

        }


        [HttpGet]
        public async Task<IEnumerable<ObraSocialDtoOut>> Get()
        {
            return await _service.GetAll();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ObraSocialDtoOut>> GetById(int id)
        {
            var obraSocial = await _service.GetDtoById(id);

            if (obraSocial is null)
                return ObraSocialNotFound(id);

            return obraSocial;
        }


        //AGREGAR
        [HttpPost]
        public async Task<IActionResult> Create(ObraSocialDtoIn obraSocial)
        {
            var newCobranza = await _service.Create(obraSocial);


            return CreatedAtAction(nameof(GetById), new { id = newCobranza.Id }, newCobranza);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ObraSocialDtoIn obraSocial)
        {
            if (id != obraSocial.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({obraSocial.Id}) del cuerpo de la solicitud." });

            var cobranzaToUpdate = await _service.GetById(id);

            if (cobranzaToUpdate is not null)
            {
                await _service.Update(id, obraSocial);
                return NoContent();

            }
            else
            {
                return ObraSocialNotFound(id);

            }

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
            {
                return ObraSocialNotFound(id);

            }

        }

        [NonAction]

        public NotFoundObjectResult ObraSocialNotFound(int id)
        {
            return NotFound(new { message = $"LA obraSocial con ID = {id} no existe." });
        }

        /* [HttpGet("IdxNombre/{nombre}")]
         public async Task<ActionResult<BancoIdDtoOut>> ObtenerIdPorNombre(string nombre)
         {
             try
             {
                 var bancoId = await _service.GetIdByNombre(nombre);

                 if (bancoId is null)
                 {
                     return NotFound("No se encontró un banco con el nombre proporcionado.");
                 }
                 return bancoId;
             }
             catch (Exception ex)
             {
                 return StatusCode(500, $"Error interno del servidor: {ex.Message}");
             }
         }*/

        /* [HttpGet("IdxCodigo/{codigo}")]
         public async Task<ActionResult<BancoIdDtoOut>> ObtenerIdPorCodigo(string codigo)
         {
             try
             {
                 var bancoId = await _service.GetIdByCodigo(codigo);

                 if (bancoId is null)
                 {
                     return NotFound("No se encontró un banco con el nombre codigo.");
                 }
                 return bancoId;
             }
             catch (Exception ex)
             {
                 return StatusCode(500, $"Error interno del servidor: {ex.Message}");
             }
         }*/

    }






}