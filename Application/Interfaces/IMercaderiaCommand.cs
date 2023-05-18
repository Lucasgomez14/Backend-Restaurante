using Application.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMercaderiaCommand
    {
        Task<Mercaderia> InsertMercaderia(Mercaderia mercaderia);
        Task<Mercaderia> UpdateMercaderia(int mercaderiaId, MercaderiaRequest mercaderia);
        Task<Mercaderia> RemoveMercaderia(Mercaderia unaMercaderia);
    }
}
