using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.ComandaMercaderia;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase
{
    public class ComandaService : IComandaService
    {
        private readonly IComandaQuery _query;
        private readonly IComandaCommand _command;
        private readonly IComandaMercaderiaService _serviceComandaMercaderia;
        private readonly ITipoMercaderiaService _serviceTipMer;
        private readonly IMercaderiaQuery _queryMercaderia;
        private readonly IFormaDeEntregaQuery _formaDeEntregaQuery;

        public ComandaService(IComandaCommand comandaCommand, IComandaQuery comandaQuery, IComandaMercaderiaService comandaMercaderiaService, IMercaderiaQuery mercaderiaQuery, IFormaDeEntregaQuery formaDeEntregaQuery, ITipoMercaderiaService serviceTipoMercaderia)
        {
            _command = comandaCommand;
            _query = comandaQuery;
            _serviceComandaMercaderia = comandaMercaderiaService;
            _queryMercaderia = mercaderiaQuery;
            _formaDeEntregaQuery = formaDeEntregaQuery;
            _serviceTipMer = serviceTipoMercaderia;
        }

        public async Task<ComandaResponse> RegisterComanda(ComandaRequest unaComanda)
        {
            try
            {
                int precioTotal = 0;
                List<int> listaMercaderiasId = unaComanda.ListaMercaderiasId;
                List<Mercaderia> listaMercaderias = new List<Mercaderia>();
                List<ComandaMercaderiaResponse> listaComMerRes = new List<ComandaMercaderiaResponse>();
                if (unaComanda.FormaEntregaId < 1 || unaComanda.FormaEntregaId > 3)
                {
                    throw new ExceptionSintaxError("No existe la forma de entrega");
                }
                foreach (int idMercaderia in listaMercaderiasId)
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
                var nuevaComanda = new Comanda
                {
                    FormaEntregaId = unaComanda.FormaEntregaId,
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
                            var ComandaMercaderiaResponse = new ComandaMercaderiaResponse
                            {
                                ComandaMercaderiaId = unaComandaMercaderia.ComandaMercaderiaId,
                                nombre = unaMercaderia.Nombre,
                                precio = unaMercaderia.Precio,

                            };
                            listaComMerRes.Add(ComandaMercaderiaResponse);

                        }
                    }

                }
                return new ComandaResponse
                {
                    ComandaId = nuevaComanda.ComandaId,
                    ListaComandaMercaderiaResponse = listaComMerRes,
                    FormaEntregaResponse = new FormaEntregaResponse
                    {
                        id = nuevaComanda.FormaEntregaId,
                        descripcion = _formaDeEntregaQuery.GetFormaEntregaById(nuevaComanda.FormaEntregaId).Result.Descripcion
                    },
                    PrecioTotal = nuevaComanda.PrecioTotal,
                    Fecha = nuevaComanda.Fecha.ToString("dd/MM/yyyy"),
                };
            }
            catch (ExceptionSintaxError ex)
            {
                throw new ExceptionSintaxError("Error en la sintaxis: " + ex.Message);
            }
        }
        public async Task<List<ComandaGetResponse>> GetAllComandaByDate(string fechaString)
        {
            try
            {
                DateTime fecha = DateTime.ParseExact(fechaString, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                List<MercaderiaGetResponse> ListaTipoMercaderiaGetResponse = new List<MercaderiaGetResponse>();
                List<ComandaGetResponse> ListaComandaGetResponse = new List<ComandaGetResponse>();

                foreach (Comanda unaComanda in await _query.GetComandasByDate(fecha))
                {
                    foreach (ComandaMercaderia unaComandaMercaderia in unaComanda.ComandasMercaderia)
                    {
                        Mercaderia unaMercaderia = await _queryMercaderia.GetMercaderiaById(unaComandaMercaderia.MercaderiaId);
                        var UnaMercaderiaGetResponse = new MercaderiaGetResponse
                        {
                            id = unaComandaMercaderia.MercaderiaId,
                            nombre = unaMercaderia.Nombre,
                            Precio = unaMercaderia.Precio,
                            imagen = unaMercaderia.Imagen,
                            tipo = new TipoMercaderiaResponse
                            {
                                id = unaMercaderia.TipoMercaderiaId,
                                descripcion = _serviceTipMer.GetTipoMercaderiaById(unaMercaderia.TipoMercaderiaId).Result.Descripcion,
                            },
                        };
                        ListaTipoMercaderiaGetResponse.Add(UnaMercaderiaGetResponse);
                    }
                    var unaComandaGetResponse = new ComandaGetResponse
                    {
                        id = unaComanda.ComandaId,
                        mercaderias = ListaTipoMercaderiaGetResponse,
                        formaEntrega = new FormaEntregaResponse
                        {
                            id = unaComanda.FormaEntregaId,
                            descripcion = _formaDeEntregaQuery.GetFormaEntregaById(unaComanda.FormaEntregaId).Result.Descripcion
                        },
                        PrecioTotal = unaComanda.PrecioTotal,
                        Fecha = unaComanda.Fecha.ToString("dd/MM/yyyy"),
                    };
                    ListaComandaGetResponse.Add(unaComandaGetResponse);

                }
                return ListaComandaGetResponse;
            }
            catch (Exception)
            {
                throw new ExceptionSintaxError("Error en la sintaxis ingresada para la fecha");
            }


        }

        public async Task<ComandaGetResponse> GetComandaById(Guid comandaId)
        {
            try
            {
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
                        Precio = unaMercaderia.Precio,
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
                    formaEntrega = new FormaEntregaResponse
                    {
                        id = unaComanda.FormaEntregaId,
                        descripcion = _formaDeEntregaQuery.GetFormaEntregaById(unaComanda.FormaEntregaId).Result.Descripcion
                    },
                    PrecioTotal = unaComanda.PrecioTotal,
                    Fecha = unaComanda.Fecha.ToString("dd/MM/yyyy"),

                };
            }
            catch (ExceptionSintaxError)
            {
                throw new ExceptionSintaxError("Error en la sintaxis del id a buscar");
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

    }
}