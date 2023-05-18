using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMercaderiaQuery
    {
        Task<List<Mercaderia>> GetListMercaderia();
        Task<Mercaderia> GetMercaderiaById(int MercaderiaId);

    }
}
