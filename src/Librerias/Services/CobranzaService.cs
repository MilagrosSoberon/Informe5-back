using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class CobranzaService(agremiaciong11Context context) : ICobranzaService
    {
        private readonly agremiaciong11Context _context = context;

        public async Task<IEnumerable<CobranzaDtoOut>> GetAll()
        {
            return await _context.Cobranza.Select(c => new CobranzaDtoOut
            {
                Fecha = c.Fecha,
                Numero = c.Numero,
                Monto = c.Monto,
                Factura = c.IdFactura

            }).ToListAsync();

        }

        public async Task<CobranzaDtoOut?> GetDtoById(int id)
        {
            return await _context.Cobranza
                .Where(c => c.Id == id)
                .Select(c => new CobranzaDtoOut
                {
                    Fecha = c.Fecha,
                    Numero = c.Numero,
                    Monto = c.Monto,
                    Factura = c.IdFactura

                }).SingleOrDefaultAsync();

        }

        public async Task<Cobranza?> GetById(int id)
        {
            return await _context.Cobranza.FindAsync(id);
        }


        public async Task<Cobranza> Create(CobranzaDtoIn newCobranzaDto)
        {
            var newCobranza = new Cobranza
            {
                Fecha = newCobranzaDto.Fecha,
                Numero = newCobranzaDto.Numero,
                Monto = newCobranzaDto.Monto,
                IdFactura = newCobranzaDto.IdFactura
            };

            _context.Cobranza.Add(newCobranza);
            await _context.SaveChangesAsync();

            return newCobranza;

        }

        public async Task Update(int id, CobranzaDtoIn cobranza)
        {
            var existingCobranza = await GetById(id);

            if (existingCobranza is not null)
            {
                existingCobranza.Fecha = cobranza.Fecha;
                existingCobranza.Numero = cobranza.Numero;
                existingCobranza.Monto = cobranza.Monto;
                existingCobranza.IdFactura = cobranza.IdFactura;
                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var cobranzaToDelete = await GetById(id);

            if (cobranzaToDelete is not null)
            {

                _context.Cobranza.Remove(cobranzaToDelete);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<int> GetLastNumber()
        {
            var ultimoNumero = await _context.Cobranza.MaxAsync(f => f.Numero);
            return ultimoNumero;
        }
    }
}
