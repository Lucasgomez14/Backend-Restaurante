using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IComandaQuery
    {
        Task<List<Comanda>> GetListComanda();
        Task<Comanda> GetComandaById(Guid comandaId);
        Task<List<Comanda>> GetComandasByDate (DateTime fecha);
    }
}
