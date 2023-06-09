﻿using Application.Request;
using Application.Response;

namespace Application.Interfaces
{
    public interface IComandaService
    {
        Task<ComandaResponse> RegisterComanda(ComandaRequest unaComanda);
        Task<List<ComandaResponse>> GetAllComandaByDate(string fecha);
        Task<ComandaGetResponse> GetComandaById(Guid comandaId);
    }
}
