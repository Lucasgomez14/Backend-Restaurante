namespace Application.Interfaces
{
    public interface IComandaMercaderiaCommand
    {
        Task<bool> InsertComandaMercaderia(Domain.Entities.ComandaMercaderia comandaMercaderia);
    }
}
