using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IComandaService
    {
        Task<ComandaResponse> RegisterComanda(ComandaRequest unaComanda);
        Task<List<ComandaGetResponse>> GetAllComandaByDate(string fecha);
        Task<ComandaGetResponse> GetComandaById(Guid comandaId);
    }
}
