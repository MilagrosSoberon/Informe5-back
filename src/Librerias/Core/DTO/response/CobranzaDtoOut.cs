namespace Core.DTO.response
{
    public class CobranzaDtoOut
    {
        public required DateTime Fecha { get; set; }     
        public required float Monto { get; set; }   
        public required int Numero { get; set; }

        public required int Factura { get; set; }


    }
}
