using Application.Request;
using Application.Response;

namespace Application.Interfaces
{
    public interface IMercaderiaService
    {
        Task<MercaderiaResponse> RegisterMercaderia(MercaderiaRequest unaMercaderia);
        Task<MercaderiaResponse> ModifyMercaderia(int mercaderiaId, MercaderiaRequest mercaderia);
        Task<MercaderiaResponse> DeleteMercaderia(int mercaderiaId);
        Task<MercaderiaResponse> GetMercaderiaById(int mercaderiaId);
        Task<List<MercaderiaGetResponse>> GetMercaderiaByFilter(int? tipoMercaderia, string? nombre, string? orden);

    }
}
