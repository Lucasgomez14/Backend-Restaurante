using Application.Interfaces;
using Domain.Entities;
using Infaestructure.Persistence.Config;
using Microsoft.EntityFrameworkCore;

namespace Infaestructure.Querys
{
    public class TipoMercaderiaQuery : ITipoMercaderiaQuery
    {
        private readonly RestauranteBD _context;

        public TipoMercaderiaQuery(RestauranteBD context)
        {
            _context = context;
        }
        public async Task<TipoMercaderia> GetTipoMercaderiaById(int TipoMercaderiaId)
        {
            return await _context.TipoMercaderia.SingleAsync(x => x.TipoMercaderiaId.Equals(TipoMercaderiaId));
        }
    }
}
