using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Domain.Entities;
using Infaestructure.Persistence.Config;
using Microsoft.EntityFrameworkCore;

namespace Infaestructure.Command
{
    public class MercaderiaCommand : IMercaderiaCommand
    {
        private readonly RestauranteBD _context;

        public MercaderiaCommand(RestauranteBD DBContext)
        {
            _context = DBContext;
        }
        public async Task<Mercaderia> InsertMercaderia(Mercaderia mercaderia)
        {
            try
            {
                _context.Add(mercaderia);
                await _context.SaveChangesAsync();
                return mercaderia;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("El tipo de mercadería recibido no existe en la base de datos");
            }
        }

        public async Task<Mercaderia> RemoveMercaderia(Mercaderia unaMercaderia)
        {
            try
            {
                _context.Remove(unaMercaderia);
                await _context.SaveChangesAsync();
                return unaMercaderia;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("No se pudo eliminar la mercaderia");
            }

        }


        public async Task<Mercaderia> UpdateMercaderia(int mercaderiaId, MercaderiaRequest mercaderia)
        {
            try
            {
                var mercaderiaToUpdate = await _context.Mercaderia.FirstOrDefaultAsync(m => m.MercaderiaID == mercaderiaId);
                mercaderiaToUpdate.TipoMercaderiaId = mercaderia.tipo;
                mercaderiaToUpdate.Preparacion = mercaderia.preparacion;
                mercaderiaToUpdate.Imagen = mercaderia.imagen;
                mercaderiaToUpdate.Ingredientes = mercaderia.ingredientes;
                mercaderiaToUpdate.Nombre = mercaderia.nombre;
                mercaderiaToUpdate.Precio = mercaderia.precio;
                await _context.SaveChangesAsync();

                return mercaderiaToUpdate;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("El tipo de mercadería recibido no existe en la base de datos");
            }

        }
    }
}
