using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Infaestructure.Persistence.Config;
using Microsoft.EntityFrameworkCore;

namespace Infaestructure.Querys
{
    public class MercaderiaQuery : IMercaderiaQuery
    {
        private readonly RestauranteBD _context;

        public MercaderiaQuery(RestauranteBD context)
        {
            _context = context;
        }
        public async Task<Mercaderia> GetMercaderiaById(int MercaderiaId)
        {
            try
            {
                Mercaderia result = await _context.Mercaderia.SingleOrDefaultAsync(x => x.MercaderiaID == MercaderiaId);
                return result;
            }
            catch (DbUpdateException)
            {
                throw new ExceptionNotFound("No se encontró la mercaderia solicitada");
            }
        }

        public async Task<List<Mercaderia>> GetListMercaderia()
        {
            return await _context.Mercaderia.ToListAsync();
        }
    }
}
