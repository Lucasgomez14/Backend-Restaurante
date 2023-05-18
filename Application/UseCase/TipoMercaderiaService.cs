using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
