using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//request es pedir
namespace Application.Request
{
    public class ComandaRequest
    {
        public List<int> ListaMercaderiasId { get; set; }
        public int FormaEntregaId { get; set; }

    }
}
