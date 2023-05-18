using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class MercaderiaResponse
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public TipoMercaderiaResponse TipoMercaderiaResponse { get; set; }  
        public int Precio { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Imagen { get; set; }

    }
}
