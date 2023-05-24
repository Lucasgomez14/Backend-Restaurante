using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMercaderiaQuery
    {
        Task<List<Mercaderia>> GetListMercaderia();
        Task<Mercaderia> GetMercaderiaById(int MercaderiaId);
        Task<List<Mercaderia>> SearchLikeName(string Nombre);
    }
}
