using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCase
{
    public class TipoMercaderiaService : ITipoMercaderiaService
    {
        private readonly ITipoMercaderiaQuery _query;

        public TipoMercaderiaService(ITipoMercaderiaQuery query)
        {
            _query = query;

        }
        public async Task<TipoMercaderia> GetTipoMercaderiaById(int TipoMercaderiaId)
        {
            return await _query.GetTipoMercaderiaById(TipoMercaderiaId);
        }
        public async Task<int> GetCantidadTipoMercaderias()
        {
            return await _query.GetCantidadTotal();
        }

    }
}
