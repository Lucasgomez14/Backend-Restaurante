using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ComandaMercaderia
    {
            public int ComandaMercaderiaId { get; set; }
            public Mercaderia Mercaderia { get; set; }
            public int MercaderiaId { get; set; }
            public Guid ComandaId { get; set; }
            public Comanda Comanda { get; set; }
        
    }
}
