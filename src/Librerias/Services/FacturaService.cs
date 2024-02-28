using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using Services;
using Microsoft.EntityFrameworkCore;
namespace Services
{
    public class FacturaService : IFacturaService
    {

        private readonly agremiaciong11Context _context;
        public FacturaService(agremiaciong11Context context)
        {
            _context = context;

        }

        public async Task<IEnumerable<FacturaDtoOut>> GetAll()
        {
            return await _context.Factura.Select(f => new FacturaDtoOut
            {
                Fecha = f.Fecha,
                FechaVencimiento = f.FechaVencimiento,
                Numero = f.Numero,
                ImporteTotal = f.ImporteTotal,
                EstadoPago = f.EstadoPago.Descripcion,
                ObraSocial = f.ObraSocial.Nombre
            }).ToListAsync();

        }

        public async Task<FacturaDtoOut?> GetDtoById(int id)
        {
            return await _context.Factura
                .Where(f => f.Id == id)
                .Select(f => new FacturaDtoOut
                {
                    Fecha = f.Fecha,
                    FechaVencimiento = f.FechaVencimiento,
                    Numero = f.Numero,
                    ImporteTotal = f.ImporteTotal,
                    EstadoPago = f.EstadoPago.Descripcion,
                    ObraSocial = f.ObraSocial.Nombre
                }).SingleOrDefaultAsync();

        }

        public async Task<Factura?> GetById(int id)
        {
            return await _context.Factura.FindAsync(id);
        }

        public async Task<Factura> Create(FacturaDtoIn newFacturaDTO)
        {
            var newFactura = new Factura();
            newFactura.Id = newFacturaDTO.Id;
            newFactura.Fecha = newFacturaDTO.Fecha;
            newFactura.FechaVencimiento = newFacturaDTO.FechaVencimiento;
            newFactura.Numero = newFacturaDTO.Numero;
            newFactura.ImporteTotal = newFacturaDTO.ImporteTotal;
            newFactura.IdEstadoPago = newFacturaDTO.IdEstadoPago;
            newFactura.IdObraSocial = newFacturaDTO.IdObraSocial;

            _context.Factura.Add(newFactura);
            await _context.SaveChangesAsync();

            return newFactura;

        }

        public async Task Update(int id, FacturaDtoIn factura)
        {
            var existingFactura = await GetById(id);

            if (existingFactura is not null)
            {
                existingFactura.Id = factura.Id;
                existingFactura.Fecha = factura.Fecha;
                existingFactura.FechaVencimiento = factura.FechaVencimiento;
                existingFactura.Numero = factura.Numero;
                existingFactura.ImporteTotal = factura.ImporteTotal;
                existingFactura.IdEstadoPago = factura.IdEstadoPago;
                existingFactura.IdObraSocial = factura.IdObraSocial;


                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var facturaToDelete = await GetById(id);

            if (facturaToDelete is not null)
            {

                _context.Factura.Remove(facturaToDelete);
                await _context.SaveChangesAsync();
            }

        }

       /* public async Task<FacturaIdDtoOut> GetIdByNombre(string nombre)
        {
            var banco = await _context.Banco
                .Where(b => b.Nombre == nombre)
                .Select(b => new BancoIdDtoOut { Id = b.Id })
                .SingleOrDefaultAsync();

            return banco;
        }*/

        /*public async Task<BancoIdDtoOut> GetIdByCodigo(string codigo)
        {
            var banco = await _context.Banco
                .Where(b => b.Codigo == codigo)
                .Select(b => new BancoIdDtoOut { Id = b.Id })
                .SingleOrDefaultAsync();

            return banco;
        }*/
    }
}