namespace Application.Interfaces.ComandaMercaderia
{
    public interface IComandaMercaderiaService
    {
        Task<bool> RegisterComandaMercaderia(Domain.Entities.ComandaMercaderia comandaMercaderia);
    }
}
