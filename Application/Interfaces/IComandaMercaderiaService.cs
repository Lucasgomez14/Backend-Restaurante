using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ComandaMercaderia
{
    public interface IComandaMercaderiaService
    {
        Task<bool> RegisterComandaMercaderia(Domain.Entities.ComandaMercaderia comandaMercaderia);
    }
}
