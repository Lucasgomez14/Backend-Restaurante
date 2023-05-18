using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Infaestructure.Persistence.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infaestructure.Command
{
    public class ComandaCommand : IComandaCommand
    {
        private readonly RestauranteBD _context;

        public ComandaCommand(RestauranteBD DBContext)
        {
            this._context = DBContext;
        }

        public async Task<bool> InsertComanda(Comanda comanda)
        {
            try
            {
                _context.Add(comanda);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new ExceptionSintaxError("No se pudo registrar la comanda");
            }
        }
    }
}