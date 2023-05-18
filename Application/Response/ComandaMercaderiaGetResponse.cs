using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.response
{
    public class ComandaMercaderiaGetResponse
    {
        public int MercaderiaID { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
    }
}
