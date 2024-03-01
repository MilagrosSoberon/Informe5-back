namespace Core.DTO.response
{
    public class FacturaDtoOut
    {
        public required DateTime Fecha { get; set; }
        public required DateTime FechaVencimiento { get; set; }

        public required int Numero { get; set; }

        public required float ImporteTotal { get; set; }

        public required string EstadoPago { get; set; } 
        public required string ObraSocial { get; set; }

    }

    public class FacturaIdDtoOut
    {
        public int Id { get; set; }
    }

}
