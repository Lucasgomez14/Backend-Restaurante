using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITipoMercaderiaQuery
    {
        public Task<TipoMercaderia> GetTipoMercaderiaById(int TipoMercaderiaId);
        public Task<int> GetCantidadTotal();
    }
}
