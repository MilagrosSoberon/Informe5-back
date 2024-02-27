
using Core.DTO.request;
using Core.DTO.response;
using Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Services
{
    public class ObraSocialService(agremiaciong11Context context) : IObraSocialService
    {
        private readonly agremiaciong11Context _context = context;

        public async Task<IEnumerable<ObraSocialDtoOut>> GetAll()
        {
            return await _context.ObraSocial.Select(o => new ObraSocialDtoOut
            {
                Nombre = o.Nombre
            }).ToListAsync();

        }

        public async Task<ObraSocialDtoOut?> GetDtoById(int id)
        {
            return await _context.ObraSocial
                .Where(o => o.Id == id)
                .Select(o => new ObraSocialDtoOut
                {
                    Nombre = o.Nombre

                }).SingleOrDefaultAsync();

        }

        public async Task<ObraSocial?> GetById(int id)
        {
            return await _context.ObraSocial.FindAsync(id);
        }


        public async Task<ObraSocial> Create(ObraSocialDtoIn newObraSocialDto)
        {
            var newObraSocial = new ObraSocial
            {
                Nombre = newObraSocialDto.Nombre
            };

            _context.ObraSocial.Add(newObraSocial);
            await _context.SaveChangesAsync();

            return newObraSocial;

        }

        public async Task Update(int id, ObraSocialDtoIn obraSocial)
        {
            var existingObraSocial = await GetById(id);

            if (existingObraSocial is not null)
            {

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
    }
}
