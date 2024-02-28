using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using Services;
using Microsoft.EntityFrameworkCore;
namespace Services
{
    public class ObraSocialService : IObraSocialService
    {

        private readonly agremiaciong11Context _context;
        public ObraSocialService(agremiaciong11Context context)
        {
            _context = context;

        }

        public async Task<IEnumerable<ObraSocialDtoOut>> GetAll()
        {
            return await _context.ObraSocial.Select(ob => new ObraSocialDtoOut
            {
                Nombre = ob.Nombre
            }).ToListAsync();

        }

        public async Task<ObraSocialDtoOut?> GetDtoById(int id)
        {
            return await _context.ObraSocial
                .Where(ob => ob.Id == id)
                .Select(ob => new ObraSocialDtoOut
                {
                    Nombre = ob.Nombre
                }).SingleOrDefaultAsync();

        }

        public async Task<ObraSocial?> GetById(int id)
        {
            return await _context.ObraSocial.FindAsync(id);
        }

        public async Task<ObraSocial> Create(ObraSocialDtoIn newObraSocialDTO)
        {
            var newObraSocial = new ObraSocial();
            newObraSocial.Id = newObraSocialDTO.Id;
            newObraSocial.Nombre = newObraSocialDTO.Nombre;

            _context.ObraSocial.Add(newObraSocial);
            await _context.SaveChangesAsync();

            return newObraSocial;

        }

        public async Task Update(int id, ObraSocialDtoIn obraSocial)
        {
            var existingObraSocial = await GetById(id);

            if (existingObraSocial is not null)
            {
                existingObraSocial.Id = obraSocial.Id;
                existingObraSocial.Nombre = obraSocial.Nombre;



                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var obraSocialToDelete = await GetById(id);

            if (obraSocialToDelete is not null)
            {

                _context.ObraSocial.Remove(obraSocialToDelete);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<ObraSocialIdDtoOut> GetIdByNombre(string nombre)
        {
            var obraSocial = await _context.ObraSocial
                .Where(b => b.Nombre == nombre)
                .Select(b => new ObraSocialIdDtoOut { Id = b.Id })
                .SingleOrDefaultAsync();

            return obraSocial;
        }
    }
}