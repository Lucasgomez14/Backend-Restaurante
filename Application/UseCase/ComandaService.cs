using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.ComandaMercaderia;
using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Application.UseCase
{
    public class ComandaService : IComandaService
    {
        private readonly IComandaQuery _query;
        private readonly IComandaCommand _command;
        private readonly IComandaMercaderiaService _serviceComandaMercaderia;
        private readonly ITipoMercaderiaService _serviceTipMer;
        private readonly IMercaderiaQuery _queryMercaderia;
        private readonly IFormaDeEntregaService _formaDeEntregaService;

        public ComandaService(IComandaCommand comandaCommand, IComandaQuery comandaQuery, IComandaMercaderiaService comandaMercaderiaService, IMercaderiaQuery mercaderiaQuery, IFormaDeEntregaService formaDeEntregaService, ITipoMercaderiaService serviceTipoMercaderia)
        {
            _command = comandaCommand;
            _query = comandaQuery;
            _serviceComandaMercaderia = comandaMercaderiaService;
            _queryMercaderia = mercaderiaQuery;
            _formaDeEntregaService = formaDeEntregaService;
            _serviceTipMer = serviceTipoMercaderia;
        }

        public async Task<ComandaResponse> RegisterComanda(ComandaRequest unaComanda)
        {
            try
            {
                int precioTotal = 0;
                List<Mercaderia> listaMercaderias = new List<Mercaderia>();
                List<MercaderiaComandaResponse> listaComMerRes = new List<MercaderiaComandaResponse>();
                await Verify400SintaxFormaEntrega(unaComanda.formaEntrega);

                foreach (int idMercaderia in unaComanda.mercaderias)
                {
                    await Verify400SintaxComandaAndAdd(idMercaderia, listaMercaderias, precioTotal);   
                }
                var nuevaComanda = new Comanda
                {
                    FormaEntregaId = unaComanda.formaEntrega,
                    Fecha = DateTime.Now.Date,
                    PrecioTotal = precioTotal,
                    ComandasMercaderia = new List<ComandaMercaderia>()
                };

                bool insertarComanda = await _command.InsertComanda(nuevaComanda);

                if (insertarComanda)
                {
                    foreach (Mercaderia unaMercaderia in listaMercaderias)
                    {
                        ComandaMercaderia unaComandaMercaderia = new ComandaMercaderia { Mercaderia = unaMercaderia, ComandaId = nuevaComanda.ComandaId };
                        bool insertarComandaMercaderia = await _serviceComandaMercaderia.RegisterComandaMercaderia(unaComandaMercaderia);

                        if (insertarComandaMercaderia)
                        {

                            nuevaComanda.ComandasMercaderia.Add(unaComandaMercaderia);
                            var ComandaMercaderiaResponse = new MercaderiaComandaResponse
                            {
                                id = unaComandaMercaderia.ComandaMercaderiaId,
                                nombre = unaMercaderia.Nombre,
                                precio = (double)unaMercaderia.Precio,

                            };
                            listaComMerRes.Add(ComandaMercaderiaResponse);

                        }
                    }

                }
                return new ComandaResponse
                {
                    id = nuevaComanda.ComandaId.ToString(),
                    mercaderias = listaComMerRes,
                    formaEntrega = new Response.FormaEntrega
                    {
                        id = nuevaComanda.FormaEntregaId,
                        descripcion = _formaDeEntregaService.GetFormaEntregaById(nuevaComanda.FormaEntregaId).Result.Descripcion
                    },
                    total = (double)nuevaComanda.PrecioTotal,
                    fecha = nuevaComanda.Fecha.ToString("dd/MM/yyyy"),
                };
            }
            catch (ExceptionSintaxError ex)
            {
                throw new ExceptionSintaxError("Error en la sintaxis: " + ex.Message);
            }
        }
        public async Task<List<ComandaResponse>> GetAllComandaByDate(string? fechaString)
        {
            try
            {
                List<MercaderiaComandaResponse> listaMercaderiaComandaResponse = new List<MercaderiaComandaResponse>();
                List<ComandaResponse> listaComandaResponse = new List<ComandaResponse>();
                if (fechaString == null)
                {
                   List<Comanda> listaComandas = await _query.GetListComanda();
                   await GetMercaderiaResponse(listaMercaderiaComandaResponse, listaComandaResponse, listaComandas);
                }
                else
                {
                    DateTime fecha = DateTime.Parse(fechaString);
                    List<Comanda> listaComandas = await _query.GetComandasByDate(fecha);
                    await GetMercaderiaResponse(listaMercaderiaComandaResponse, listaComandaResponse, listaComandas);
                }
                return listaComandaResponse;
            }
            catch (ExceptionSintaxError ex)
            {
                throw new ExceptionSintaxError("Error en la sintaxis ingresada para la fecha: "+ ex.Message);
            }
        }

        public async Task<ComandaGetResponse> GetComandaById(Guid comandaId)
        {
            try
            {
                if(!Guid.TryParse(comandaId.ToString(), out comandaId)) { throw new ExceptionSintaxError(); }
                if (await VerifyHTTP404Async(comandaId))
                {
                    throw new ExceptionNotFound("No existe una mercadería con ese Id");
                }
                Comanda unaComanda = await _query.GetComandaById(comandaId);
                List<MercaderiaGetResponse> ListaTipoMercaderiaGetResponse = new List<MercaderiaGetResponse>();
                foreach (ComandaMercaderia unaComandaMercaderia in unaComanda.ComandasMercaderia)
                {
                    Mercaderia unaMercaderia = await _queryMercaderia.GetMercaderiaById(unaComandaMercaderia.MercaderiaId);
                    var UnaMercaderiaGetResponse = new MercaderiaGetResponse
                    {
                        id = unaComandaMercaderia.MercaderiaId,
                        nombre = unaMercaderia.Nombre,
                        Precio = (double)unaMercaderia.Precio,
                        imagen = unaMercaderia.Imagen,
                        tipo = new TipoMercaderiaResponse
                        {
                            id = unaMercaderia.TipoMercaderiaId,
                            descripcion = _serviceTipMer.GetTipoMercaderiaById(unaMercaderia.TipoMercaderiaId).Result.Descripcion,
                        },
                    };
                    ListaTipoMercaderiaGetResponse.Add(UnaMercaderiaGetResponse);
                }
                return new ComandaGetResponse
                {
                    id = unaComanda.ComandaId,
                    mercaderias = ListaTipoMercaderiaGetResponse,
                    formaEntrega = new Response.FormaEntrega
                    {
                        id = unaComanda.FormaEntregaId,
                        descripcion = _formaDeEntregaService.GetFormaEntregaById(unaComanda.FormaEntregaId).Result.Descripcion
                    },
                    PrecioTotal = (double)unaComanda.PrecioTotal,
                    Fecha = unaComanda.Fecha.ToString("dd/MM/yyyy"),

                };
            }
            catch (ExceptionSintaxError)
            {
                throw new ExceptionSintaxError("Sintaxis inválida para el id a buscar, pruebe ingresar el id con el formato válido");
            }
            catch (ExceptionNotFound ex)
            {
                throw new ExceptionNotFound("Error en la búsqueda en la base de datos: " + ex.Message);
            }
        }

        //Método para el error HTTP 404
        private async Task<bool> VerifyHTTP404Async(Guid comandaId)
        {
            if (await _query.GetComandaById(comandaId) == null)
            {
                return true;
            }
            return false;
        }
        private async Task Verify400SintaxFormaEntrega(int formaEntregaId)
        {
            if (!int.TryParse(formaEntregaId.ToString(), out formaEntregaId)) { throw new ExceptionSintaxError("El tipo de forma de entrega no es un valor válido, pruebe introduciendo un entero"); }
            if (formaEntregaId < 1 || formaEntregaId > await _formaDeEntregaService.GetFormaEntregaTotal()){ throw new ExceptionSintaxError("No existe la forma de entrega ingresada"); }
        }
        private async Task Verify400SintaxComandaAndAdd (int idMercaderia, List<Mercaderia> listaMercaderias, int precioTotal)
        {
            Mercaderia mercaderia = await _queryMercaderia.GetMercaderiaById(idMercaderia);
            if (mercaderia != null)
            {
                precioTotal += mercaderia.Precio;
                listaMercaderias.Add(mercaderia);
            }
            else
            {
                throw new ExceptionSintaxError("Una mercaderia es inválida");
            }
        }

        private async Task GetMercaderiaResponse (List<MercaderiaComandaResponse> ListaMercaderiaComandaResponse, List<ComandaResponse> ListaComandaResponse, List<Comanda> ListaComandas)
        {
            foreach (Comanda unaComanda in ListaComandas)
            {
                foreach (ComandaMercaderia unaComandaMercaderia in unaComanda.ComandasMercaderia)
                {
                    Mercaderia unaMercaderia = await _queryMercaderia.GetMercaderiaById(unaComandaMercaderia.MercaderiaId);
                    var unaComandaMercaderiaResponse = new MercaderiaComandaResponse
                    {
                        id = unaMercaderia.MercaderiaID,
                        nombre = unaMercaderia.Nombre,
                        precio = (double)unaMercaderia.Precio,
                    };
                    ListaMercaderiaComandaResponse.Add(unaComandaMercaderiaResponse);

                }
                var unaComandaResponse = new ComandaResponse
                {
                    id = unaComanda.ComandaId.ToString(),
                    mercaderias = ListaMercaderiaComandaResponse,
                    formaEntrega = new Response.FormaEntrega
                    {
                        id = unaComanda.FormaEntregaId,
                        descripcion = _formaDeEntregaService.GetFormaEntregaById(unaComanda.FormaEntregaId).Result.Descripcion
                    },
                    total = (double)unaComanda.PrecioTotal,
                    fecha = unaComanda.Fecha.ToString("dd/MM/yyyy"),
                };
                ListaComandaResponse.Add(unaComandaResponse);
            }
        }

    }
}