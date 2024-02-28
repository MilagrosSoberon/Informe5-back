
using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Services
{
    public class EstadoPagoService(agremiaciong11Context context) : IEstadoPagoService
    {
        private readonly agremiaciong11Context _context = context;

        public async Task<IEnumerable<EstadoPagoDtoOut>> GetAll()
        {
            return await _context.EstadoPago.Select(e => new EstadoPagoDtoOut
            {
                Descripcion = e.Descripcion
            }).ToListAsync();

        }

        public async Task<EstadoPagoDtoOut?> GetDtoById(int id)
        {
            return await _context.EstadoPago
                .Where(e => e.Id == id)
                .Select(e => new EstadoPagoDtoOut
                {
                    Descripcion = e.Descripcion

                }).SingleOrDefaultAsync();

        }

        public async Task<EstadoPago?> GetById(int id)
        {
            return await _context.EstadoPago.FindAsync(id);
        }


        public async Task<EstadoPago> Create(EstadoPagoDtoIn newEstadoPagoDto)
        {
            var newEstadoPago = new EstadoPago
            {
                Descripcion = newEstadoPagoDto.Descripcion
            };

            _context.EstadoPago.Add(newEstadoPago);
            await _context.SaveChangesAsync();

            return newEstadoPago;

        }

        public async Task Update(int id, EstadoPagoDtoIn estadoPago)
        {
            var existingEstadoPago = await GetById(id);

            if (existingEstadoPago is not null)
            {

                existingEstadoPago.Descripcion = estadoPago.Descripcion;
                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var estadoPagoToDelete = await GetById(id);

            if (estadoPagoToDelete is not null)
            {

                _context.EstadoPago.Remove(estadoPagoToDelete);
                await _context.SaveChangesAsync();
            }

        }
    }
}
