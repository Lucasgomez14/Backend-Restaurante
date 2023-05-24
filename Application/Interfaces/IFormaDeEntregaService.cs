using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFormaDeEntregaService
    {
        public Task<FormaEntrega> GetFormaEntregaById(int id);
        public Task<int> GetFormaEntregaTotal();
    }
}
