using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface ICobranzaService
    {
        Task<IEnumerable<CobranzaDtoOut>> GetAll();
        Task<CobranzaDtoOut?> GetDtoById(int id);
        Task<Cobranza?> GetById(int id);
        Task<Cobranza> Create(CobranzaDtoIn newCobranzaDto);
        Task Update(int id, CobranzaDtoIn cobranza);
        Task Delete(int id);
    }
}