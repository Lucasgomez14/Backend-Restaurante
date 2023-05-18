using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Infaestructure.Persistence.Config;
using Microsoft.EntityFrameworkCore;

namespace Infaestructure.Querys
{
    public class ComandaQuerys : IComandaQuery
    {
        private readonly RestauranteBD _context;

        public ComandaQuerys(RestauranteBD context)
        {
            _context = context;
        }

        public async Task<Comanda> GetComandaById(Guid comandaId)
        {
            try
            {
                var Comanda = await _context.Comanda
                .Include(cm => cm.ComandasMercaderia)
                .SingleOrDefaultAsync(c => c.ComandaId.Equals(comandaId));
                return Comanda;
            }
            catch (DbUpdateException)
            {
                throw new ExceptionSintaxError("No se encontró la comanda solicitada");
            }


        }

        public async Task<List<Comanda>> GetComandasByDate(DateTime fecha)
        {
            try
            {
                List<Comanda> comandasPorFecha = await _context.Comanda
               .Include(c => c.ComandasMercaderia)
               .Where(c => c.Fecha.Date == fecha.Date)
               .ToListAsync();
                return comandasPorFecha;
            }
            catch (DbUpdateException)
            {
                throw new ExceptionSintaxError("No se encontró la comanda solicitada");
            }

        }

        public async Task<List<Comanda>> GetListComanda()
        {
            try
            {
                List<Comanda> todasLasComandas = await _context.Comanda
                .Include(c => c.ComandasMercaderia)
                .ToListAsync();
                return todasLasComandas;
            }
            catch (DbUpdateException)
            {
                throw new ExceptionSintaxError("No existen comandas en la base");
            }
        }
    }
}
