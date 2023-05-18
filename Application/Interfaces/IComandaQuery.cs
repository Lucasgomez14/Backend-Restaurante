using Domain.Entities;

namespace Application.Interfaces
{
    public interface IComandaQuery
    {
        Task<List<Comanda>> GetListComanda();
        Task<Comanda> GetComandaById(Guid comandaId);
        Task<List<Comanda>> GetComandasByDate(DateTime fecha);
    }
}
