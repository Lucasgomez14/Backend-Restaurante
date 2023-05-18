using Application.Interfaces;
using Domain.Entities;
using Infaestructure.Persistence.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infaestructure.Querys
{
    public class FormaDeEntregaQuery : IFormaDeEntregaQuery
    {
        private readonly RestauranteBD _context;

        public FormaDeEntregaQuery(RestauranteBD context)
        {
            _context = context;
        }
        public async Task<FormaEntrega> GetFormaEntregaById(int id)
        {
            return await _context.FormasEntrega.SingleAsync(x => x.FormaEntregaId == id);
        }
    }
}
