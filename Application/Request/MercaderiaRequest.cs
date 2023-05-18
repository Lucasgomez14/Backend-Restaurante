using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request
{
    public class MercaderiaRequest
    {
        public string Nombre { get; set; }
        public int TipoMercaderiaId { get; set; }
        public int Precio { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Imagen { get; set; }
     
    }
}
