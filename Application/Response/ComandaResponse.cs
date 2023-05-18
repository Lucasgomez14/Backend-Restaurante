using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class ComandaResponse
    {
        public Guid ComandaId { get; set; }
        public List<ComandaMercaderiaResponse> ListaComandaMercaderiaResponse { get; set; }
        public FormaEntregaResponse FormaEntregaResponse { get; internal set; }
        public int PrecioTotal { get; set; }
        public string Fecha { get; set; }
        
    }
}
