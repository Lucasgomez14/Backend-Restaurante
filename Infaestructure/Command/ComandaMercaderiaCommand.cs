using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Infaestructure.Persistence.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infaestructure.Command
{
    public class ComandaMercaderiaCommand : IComandaMercaderiaCommand
    {
        private readonly RestauranteBD _context;

        public ComandaMercaderiaCommand(RestauranteBD DBContext)
        {
            _context = DBContext;
        }
        public async Task<bool> InsertComandaMercaderia(ComandaMercaderia comandaMercaderia)
        {
            try
            {
                _context.Add(comandaMercaderia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new ExceptionSintaxError("No se pudo registrar la comandaMercaderia");
            }
        }
    }
}
