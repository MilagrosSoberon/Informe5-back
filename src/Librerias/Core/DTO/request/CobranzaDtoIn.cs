using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO.request
{
    public class CobranzaDtoIn
    {
        public int Id { get; set; }
            public DateTime Fecha { get; set; }

            public float Monto { get; set; }

            public int Numero { get; set; }

            public int IdFactura { get; set; }

        
    }
}
