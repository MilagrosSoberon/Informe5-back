using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface IObraSocialService
    {
        Task<IEnumerable<ObraSocialDtoOut>> GetAll();
        Task<ObraSocialDtoOut?> GetDtoById(int id);
        Task<ObraSocial?> GetById(int id);
        Task<ObraSocial> Create(ObraSocialDtoIn newObraSocialDTO);
        Task Update(int id, ObraSocialDtoIn obraSocial);
        Task Delete(int id);
        Task<ObraSocialIdDtoOut> GetIdByNombre(string nombre);

    }
}