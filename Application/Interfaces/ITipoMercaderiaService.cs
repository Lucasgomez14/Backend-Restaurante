using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITipoMercaderiaService
    {
        public Task<TipoMercaderia> GetTipoMercaderiaById(int TipoMercaderiaId);
    }
}
