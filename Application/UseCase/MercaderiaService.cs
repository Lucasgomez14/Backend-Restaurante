using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase
{
    public class MercaderiaService : IMercaderiaService
    {
        private readonly IMercaderiaQuery _query;
        private readonly IMercaderiaCommand _command;
        private readonly ITipoMercaderiaService _serviceTipMer;
        private readonly IComandaQuery _comandaQuery;

        public MercaderiaService(IMercaderiaQuery query, IMercaderiaCommand command, ITipoMercaderiaService serviceTipMer, IComandaQuery comandaQuery)
        {
            _query = query;
            _command = command;
            _serviceTipMer = serviceTipMer;
            _comandaQuery = comandaQuery;
        }

        public async Task<MercaderiaResponse> RegisterMercaderia(MercaderiaRequest unaMercaderia)
        {
            try
            {
                string fixNombre = unaMercaderia.Nombre;
                fixNombre = char.ToUpper(fixNombre[0]) + fixNombre.Substring(1);
                var nuevaMercaderia = new Mercaderia
                {
                    Nombre = fixNombre,
                    TipoMercaderiaId = unaMercaderia.TipoMercaderiaId,
                    Precio = unaMercaderia.Precio,
                    Ingredientes = unaMercaderia.Ingredientes,
                    Preparacion = unaMercaderia.Preparacion,
                    Imagen = unaMercaderia.Imagen,
                };
                if (await VerifyHTTP409InsertAsync(nuevaMercaderia))
                {
                    throw new Conflict("La mercadería ya existe");
                }
                Mercaderia mercaderiaInsertar = await _command.InsertMercaderia(nuevaMercaderia);
                return await Task.FromResult(new MercaderiaResponse
                {
                    ID = mercaderiaInsertar.MercaderiaID,
                    Nombre = mercaderiaInsertar.Nombre,
                    TipoMercaderiaResponse = new TipoMercaderiaResponse
                    {
                        descripcion = _serviceTipMer.GetTipoMercaderiaById(mercaderiaInsertar.TipoMercaderiaId).Result.Descripcion,
                        id = mercaderiaInsertar.TipoMercaderiaId,
                    },
                    Precio = mercaderiaInsertar.Precio,
                    Ingredientes = mercaderiaInsertar.Ingredientes,
                    Preparacion = mercaderiaInsertar.Preparacion,
                    Imagen = mercaderiaInsertar.Imagen,
                });
            }
            catch (Conflict ex) { throw new Conflict("Error en la implementación a la base de datos: " + ex.Message); }
            catch (ExceptionSintaxError) { throw new ExceptionSintaxError("Error en la sintaxis de la mercadería en el registro"); }

        }

        public async Task<MercaderiaResponse> ModifyMercaderia(int mercaderiaId, MercaderiaRequest mercaderia)
        {
            try
            {
                mercaderia.Nombre = char.ToUpper(mercaderia.Nombre[0]) + mercaderia.Nombre.Substring(1);

                if (await VerifyHTTP404Async(mercaderiaId)) { throw new ExceptionNotFound("No existe una mercadería con ese Id"); }
                if (await VerifyHTTP409ModifyAsync(mercaderia.Nombre, mercaderiaId)) { throw new Conflict("Existe otra mercadería con el nombre a modificar"); }

                var unaMercaderia = await _command.UpdateMercaderia(mercaderiaId, mercaderia);
                return await Task.FromResult(new MercaderiaResponse
                {
                    ID = unaMercaderia.MercaderiaID,
                    Nombre = unaMercaderia.Nombre,
                    TipoMercaderiaResponse = new TipoMercaderiaResponse
                    {
                        descripcion = _serviceTipMer.GetTipoMercaderiaById(unaMercaderia.TipoMercaderiaId).Result.Descripcion,
                        id = unaMercaderia.TipoMercaderiaId,
                    },
                    Precio = unaMercaderia.Precio,
                    Ingredientes = unaMercaderia.Ingredientes,
                    Preparacion = unaMercaderia.Preparacion,
                    Imagen = unaMercaderia.Imagen,
                });
            }
            catch (Conflict ex) { throw new Conflict("Error en la implementación a la base de datos: " + ex.Message); }
            catch (ExceptionNotFound ex) { throw new ExceptionNotFound("Error la busqueda en la base de datos: " + ex.Message); }
            catch (ExceptionSintaxError) { throw new ExceptionSintaxError("Error en la sintaxis de la mercadería a modificar"); }

        }


        public async Task<MercaderiaResponse> DeleteMercaderia(int mercaderiaId)
        {
            try
            {
                if (await VerifyHTTP404Async(mercaderiaId)) { throw new Conflict("No existe una mercadería con ese Id"); }

                if (await VerifyIfExistInComanda(mercaderiaId)) { throw new Conflict("No puede borrarse la mercadería debido a que una comanda la contiene en su pedido"); }

                Mercaderia mercaderiaARemover = await _command.RemoveMercaderia(await _query.GetMercaderiaById(mercaderiaId));
                return new MercaderiaResponse
                {
                    ID = mercaderiaARemover.MercaderiaID,
                    Nombre = mercaderiaARemover.Nombre,
                    TipoMercaderiaResponse = new TipoMercaderiaResponse
                    {
                        descripcion = _serviceTipMer.GetTipoMercaderiaById(mercaderiaARemover.TipoMercaderiaId).Result.Descripcion,
                        id = mercaderiaARemover.TipoMercaderiaId,
                    },
                    Precio = mercaderiaARemover.Precio,
                    Ingredientes = mercaderiaARemover.Ingredientes,
                    Preparacion = mercaderiaARemover.Preparacion,
                    Imagen = mercaderiaARemover.Imagen,
                };
            }
            catch (Conflict ex) { throw new Conflict("Error en la base de datos: " + ex.Message); }
            catch (ExceptionSintaxError) { throw new ExceptionSintaxError("Id incorrecto"); }
        }


        public async Task<MercaderiaResponse> GetMercaderiaById(int mercaderiaId)
        {
            try
            {
                if (await VerifyHTTP404Async(mercaderiaId)) { throw new ExceptionNotFound("No existe una mercadería con ese Id"); }

                Mercaderia unaMercaderia = await _query.GetMercaderiaById(mercaderiaId);
                return new MercaderiaResponse
                {
                    ID = unaMercaderia.MercaderiaID,
                    Nombre = unaMercaderia.Nombre,
                    TipoMercaderiaResponse = new TipoMercaderiaResponse
                    {
                        descripcion = _serviceTipMer.GetTipoMercaderiaById(unaMercaderia.TipoMercaderiaId).Result.Descripcion,
                        id = unaMercaderia.TipoMercaderiaId,
                    },
                    Precio = unaMercaderia.Precio,
                    Ingredientes = unaMercaderia.Ingredientes,
                    Preparacion = unaMercaderia.Preparacion,
                    Imagen = unaMercaderia.Imagen,

                };
            }
            catch (ExceptionSintaxError) { throw new ExceptionSintaxError("Error en la sintaxis del id a buscar"); }
            catch (ExceptionNotFound ex) { throw new ExceptionNotFound("Error en la búsqueda en la base de datos: " + ex.Message); }

        }
        public async Task<List<MercaderiaGetResponse>> GetMercaderiaByFilter(int? tipoMercaderia, string? nombre, string? orden)
        {
            try
            {
                List<MercaderiaGetResponse> listaMerGetResponse = new();
                List<Mercaderia> listaMercaderia = await _query.GetListMercaderia();
                bool flag = true;

                if (listaMercaderia.Count() > 0)
                {
                    if (orden == null || !orden.ToUpper().Equals("ASC") && !orden.ToUpper().Equals("DES"))
                    {
                        orden = "ASC";
                    }

                    if (nombre != null && flag)
                    {
                        await GenerateMercaderiaResponseByName(listaMercaderia, listaMerGetResponse, (string)nombre);
                        flag = false;
                    }

                    if (tipoMercaderia >= 1 && tipoMercaderia <= 10 && flag)
                    {
                        await GenerateMercaderiaResponseByTipoMercaderia(listaMercaderia, listaMerGetResponse, (int)tipoMercaderia);
                        flag = false;
                    }

                    if (listaMerGetResponse.Count == 0)
                    {
                        foreach (Mercaderia unaMercaderia in listaMercaderia)
                        {
                            await GenerateMercaderiaResponse(unaMercaderia, listaMerGetResponse);
                        }

                    }

                    if (orden.ToUpper().Equals("ASC")) { listaMerGetResponse = listaMerGetResponse.OrderBy(m => m.Precio).ToList(); }
                    if (orden.ToUpper().Equals("DES")) { listaMerGetResponse = listaMerGetResponse.OrderByDescending(m => m.Precio).ToList(); }

                    return listaMerGetResponse;
                }
                else { throw new ExceptionNotFound("Base de datos vacía"); }
            }
            catch (ExceptionSintaxError) { throw new ExceptionSintaxError("Error en la sintaxis"); }
            catch (ExceptionNotFound ex) { throw new ExceptionSintaxError("Error: " + ex.Message); }
        }

        //Métodos Para GetMercaderiaByFilter
        private async Task GenerateMercaderiaResponse(Mercaderia unaMercaderia, List<MercaderiaGetResponse> listaMerGetResponse)
        {
            var unaMercaderiaGetResponse = new MercaderiaGetResponse
            {
                id = unaMercaderia.MercaderiaID,
                nombre = unaMercaderia.Nombre,
                Precio = unaMercaderia.Precio,
                tipo = new TipoMercaderiaResponse
                {
                    id = unaMercaderia.TipoMercaderiaId,
                    descripcion = (await _serviceTipMer.GetTipoMercaderiaById(unaMercaderia.TipoMercaderiaId)).Descripcion
                },
                imagen = unaMercaderia.Imagen
            };
            if (!listaMerGetResponse.Any(m => m.id == unaMercaderiaGetResponse.id))
            {
                listaMerGetResponse.Add(unaMercaderiaGetResponse);
            }
        }
        private async Task GenerateMercaderiaResponseByTipoMercaderia(List<Mercaderia> listaMercaderia, List<MercaderiaGetResponse> listaMerGetResponse, int tipoMercaderia)
        {
            foreach (Mercaderia unaMercaderia in listaMercaderia)
            {
                if (tipoMercaderia == unaMercaderia.TipoMercaderiaId)
                {
                    await GenerateMercaderiaResponse(unaMercaderia, listaMerGetResponse);
                }
            }
        }
        private async Task GenerateMercaderiaResponseByName(List<Mercaderia> listaMercaderia, List<MercaderiaGetResponse> listaMerGetResponse, string nombre)
        {
            foreach (Mercaderia unaMercaderia in listaMercaderia)
            {
                if (nombre.ToUpper() == unaMercaderia.Nombre.ToUpper())
                {
                    await GenerateMercaderiaResponse(unaMercaderia, listaMerGetResponse);
                }
            }
        }

        //Métodos para el error HTTP 409

        private async Task<bool> VerifyHTTP409InsertAsync(Mercaderia unaMercaderia)
        {
            List<Mercaderia> listaMercaderias = await _query.GetListMercaderia();
            foreach (Mercaderia mercaderia in listaMercaderias)
            {
                if (mercaderia.Nombre.Equals(unaMercaderia.Nombre))
                {
                    return true;
                }
            }
            return false;
        }
        private async Task<bool> VerifyHTTP409ModifyAsync(string nombreMercaderia, int mercaderiaId)
        {
            List<Mercaderia> listaMercaderias = await _query.GetListMercaderia();
            foreach (Mercaderia mercaderia in listaMercaderias)
            {
                if (mercaderia.Nombre.Equals(nombreMercaderia) && mercaderia.MercaderiaID != mercaderiaId)
                {
                    return true;
                }
            }
            return false;
        }

        //Método para el error HTTP 404
        private async Task<bool> VerifyHTTP404Async(int mercaderiaId)
        {
            if (await _query.GetMercaderiaById(mercaderiaId) == null)
            {
                return true;
            }
            return false;
        }

        //tendría que traer todas las comandas y de ahí que revise la lista de comandamercaderia
        //y si encuentra el mismo id con el de la lista, debería devolver true
        private async Task<bool> VerifyIfExistInComanda(int mercaderiaId)
        {
            List<Comanda> ListaComandas = await _comandaQuery.GetListComanda();
            foreach (Comanda unaComanda in ListaComandas)
            {
                foreach (ComandaMercaderia unaComandaMercaderia in unaComanda.ComandasMercaderia)
                {
                    if (mercaderiaId == unaComandaMercaderia.MercaderiaId) { return true; }
                }
            }
            return false;
        }

    }
}
