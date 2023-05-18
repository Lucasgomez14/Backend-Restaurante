using Application.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMercaderiaCommand
    {
        Task<Mercaderia> InsertMercaderia(Mercaderia mercaderia);
        Task<Mercaderia> UpdateMercaderia(int mercaderiaId, MercaderiaRequest mercaderia);
        Task<Mercaderia> RemoveMercaderia(Mercaderia unaMercaderia);
    }
}
