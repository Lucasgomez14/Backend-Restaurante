using Domain.Entities;

namespace Application.Interfaces
{
    public interface IComandaCommand
    {
        Task<bool> InsertComanda(Comanda comanda);
    }
}
