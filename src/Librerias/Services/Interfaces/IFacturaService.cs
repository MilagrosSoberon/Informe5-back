using Core.DTO.request;
using Core.DTO.response;
using Data.Models;

namespace Services
{
    public interface IFacturaService
    {
        Task<IEnumerable<FacturaDtoOut>> GetAll();
        Task<FacturaDtoOut?> GetDtoById(int id);
        Task<Factura?> GetById(int id);
        Task<Factura> Create(FacturaDtoIn newFacturaDTO);
        Task Update(int id, FacturaDtoIn factura);
        Task Delete(int id);
    }
}