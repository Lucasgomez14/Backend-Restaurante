using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFormaDeEntregaQuery
    {
        public Task<FormaEntrega> GetFormaEntregaById(int id);
        public Task<int> GetFormaEntregaTotal();
    }
}
