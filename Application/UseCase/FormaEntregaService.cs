using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class FormaEntregaService: IFormaDeEntregaService
    {
        private readonly IFormaDeEntregaQuery _query;
        public FormaEntregaService(IFormaDeEntregaQuery query)
        {
            _query = query;
        }

        public async Task<FormaEntrega> GetFormaEntregaById(int id)
        {
            return await _query.GetFormaEntregaById(id);
        }
        public async Task<int> GetFormaEntregaTotal()
        {
            return await _query.GetFormaEntregaTotal();
        }
    }
}
