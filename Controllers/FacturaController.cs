using Core.DTO.request;
using Core.DTO.response;
using Services;
using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.JsonPatch;

namespace agremiacion.Controllers

{
    [ApiController]
    [Route("[controller]")]


    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _service;

        public FacturaController(IFacturaService factura)
        {
            _service = factura;

        }


        [HttpGet]
        public async Task<IEnumerable<FacturaDtoOut>> Get()
        {
            return await _service.GetAll();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaDtoOut>> GetById(int id)
        {
            var factura = await _service.GetDtoById(id);

            if (factura is null)
                return FacturaNotFound(id);

            return factura;
        }


        //AGREGAR
        [HttpPost]
        public async Task<IActionResult> Create(FacturaDtoIn factura)
        {
            var newFactura = await _service.Create(factura);


            return CreatedAtAction(nameof(GetById), new { id = newFactura.Id }, newFactura);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FacturaDtoIn factura)
        {
            if (id != factura.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({factura.Id}) del cuerpo de la solicitud." });

            var facturaToUpdate = await _service.GetById(id);

            if (facturaToUpdate is not null)
            {
                await _service.Update(id, factura);
                return NoContent();

            }
            else
            {
                return FacturaNotFound(id);

            }

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var facturaToDelete = await _service.GetById(id);

            if (facturaToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();

            }
            else
            {
                return FacturaNotFound(id);

            }

        }

        [HttpGet("factXObraSocial/{obraSocial}")]

        public async Task<IEnumerable<FacturaDtoOut>> GetFacturaByObraSocial(string obraSocial)
        {
            return await _service.GetFacturaByObraSocial(obraSocial);

        }

        [NonAction]

        public NotFoundObjectResult FacturaNotFound(int id)
        {
            return NotFound(new { message = $"LA factura con ID = {id} no existe." });
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEP(int id, [FromQuery] int idEstadoPago)
        {
            var facturaToUpdate = await _service.GetById(id);
            if (facturaToUpdate is not null)
            {
                await _service.UpdateEstadoPago(id, idEstadoPago);
                return NoContent();

            }
            else
            {
                return FacturaNotFound(id);

            }

            

        }


    }






}