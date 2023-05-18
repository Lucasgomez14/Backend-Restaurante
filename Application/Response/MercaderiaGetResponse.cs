using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class MercaderiaGetResponse
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int Precio { get; set; }
        public TipoMercaderiaResponse tipo { get; set; }
        public string imagen { get; set; }
    }
}
