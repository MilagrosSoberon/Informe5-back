using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface IEstadoPagoService
    {
        Task<IEnumerable<EstadoPagoDtoOut>> GetAll();
        Task<EstadoPagoDtoOut?> GetDtoById(int id);
        Task<EstadoPago?> GetById(int id);
        Task<EstadoPago> Create(EstadoPagoDtoIn newEstadoPagoDto);
        Task Update(int id, EstadoPagoDtoIn estadoPago);
        Task Delete(int id);
    }
}