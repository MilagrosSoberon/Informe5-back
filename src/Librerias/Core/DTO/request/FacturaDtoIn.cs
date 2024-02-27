using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.DTO.request

{
    public class FacturaDtoIn
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public int Numero { get; set; }

        public float ImporteTotal { get; set; }

        public int IdEstadoPago { get; set; }

        public int IdObraSocial { get; set; }
    }
}
