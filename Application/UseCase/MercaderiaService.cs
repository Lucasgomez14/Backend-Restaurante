using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Linq.Expressions;

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
                try { await Verify400Sintax(unaMercaderia); }
                catch (ExceptionSintaxError ex) { throw new ExceptionSintaxError(ex.Message); }
                string fixNombre = unaMercaderia.nombre;
                fixNombre = char.ToUpper(fixNombre[0]) + fixNombre.Substring(1);
                var nuevaMercaderia = new Mercaderia
                {
                    Nombre = fixNombre,
                    TipoMercaderiaId = unaMercaderia.tipo,
                    Precio = unaMercaderia.precio,
                    Ingredientes = unaMercaderia.ingredientes,
                    Preparacion = unaMercaderia.preparacion,
                    Imagen = unaMercaderia.imagen,
                };
                
                if (await VerifyHTTP409InsertAsync(nuevaMercaderia))
                {
                    throw new Conflict("La mercadería ya existe");
                }
                Mercaderia mercaderiaInsertar = await _command.InsertMercaderia(nuevaMercaderia);
                return await Task.FromResult(new MercaderiaResponse
                {
                    id = mercaderiaInsertar.MercaderiaID,
                    nombre = mercaderiaInsertar.Nombre,
                    tipo = new TipoMercaderiaResponse
                    {
                        descripcion = _serviceTipMer.GetTipoMercaderiaById(mercaderiaInsertar.TipoMercaderiaId).Result.Descripcion,
                        id = mercaderiaInsertar.TipoMercaderiaId,
                    },
                    precio = (double)mercaderiaInsertar.Precio,
                    ingredientes = mercaderiaInsertar.Ingredientes,
                    preparacion = mercaderiaInsertar.Preparacion,
                    imagen = mercaderiaInsertar.Imagen,
                });
            }
            catch (Conflict ex) { throw new Conflict("Error en la implementación a la base de datos: " + ex.Message); }
            catch (ExceptionSintaxError ex) { throw new ExceptionSintaxError("Error en la sintaxis de la mercadería a registrar: " + ex.Message); }

        }

        public async Task<MercaderiaResponse> ModifyMercaderia(int mercaderiaId, MercaderiaRequest mercaderia)
        {
            try
            {
                try { await Verify400Sintax(mercaderia); }

                catch (ExceptionSintaxError ex) { throw new ExceptionSintaxError(ex.Message); }

                mercaderia.nombre = char.ToUpper(mercaderia.nombre[0]) + mercaderia.nombre.Substring(1);
                if (await VerifyHTTP404Async(mercaderiaId)) { throw new ExceptionNotFound("No existe una mercadería con ese Id"); }
                if (await VerifyHTTP409ModifyAsync(mercaderia.nombre, mercaderiaId)) { throw new Conflict("Existe otra mercadería con el nombre a modificar"); }

                var unaMercaderia = await _command.UpdateMercaderia(mercaderiaId, mercaderia);
                return await Task.FromResult(new MercaderiaResponse
                {
                    id = unaMercaderia.MercaderiaID,
                    nombre = unaMercaderia.Nombre,
                    tipo = new TipoMercaderiaResponse
                    {
                        descripcion = _serviceTipMer.GetTipoMercaderiaById(unaMercaderia.TipoMercaderiaId).Result.Descripcion,
                        id = unaMercaderia.TipoMercaderiaId,
                    },
                    precio = (double)unaMercaderia.Precio,
                    ingredientes = unaMercaderia.Ingredientes,
                    preparacion = unaMercaderia.Preparacion,
                    imagen = unaMercaderia.Imagen,
                });
            }
            catch (Conflict ex) { throw new Conflict("Error en la implementación a la base de datos: " + ex.Message); }
            catch (ExceptionNotFound ex) { throw new ExceptionNotFound("Error en la busqueda en la base de datos: " + ex.Message); }
            catch (ExceptionSintaxError ex) { throw new ExceptionSintaxError("Error en la sintaxis de la mercadería a modificar: "+ ex.Message); }

        }


        public async Task<MercaderiaResponse> DeleteMercaderia(int mercaderiaId)
        {
            try
            {
                if (!int.TryParse(mercaderiaId.ToString(), out mercaderiaId)) { throw new ExceptionSintaxError(); }

                if (await VerifyHTTP404Async(mercaderiaId)) { throw new ExceptionNotFound("No existe una mercadería con ese Id"); }

                if (await VerifyIfExistInComanda(mercaderiaId)) { throw new Conflict("No puede borrarse la mercadería debido a que una comanda la contiene en su pedido"); }

                Mercaderia mercaderiaARemover = await _command.RemoveMercaderia(await _query.GetMercaderiaById(mercaderiaId));
                return new MercaderiaResponse
                {
                    id = mercaderiaARemover.MercaderiaID,
                    nombre = mercaderiaARemover.Nombre,
                    tipo = new TipoMercaderiaResponse
                    {
                        descripcion = _serviceTipMer.GetTipoMercaderiaById(mercaderiaARemover.TipoMercaderiaId).Result.Descripcion,
                        id = mercaderiaARemover.TipoMercaderiaId,
                    },
                    precio = (double)mercaderiaARemover.Precio,
                    ingredientes = mercaderiaARemover.Ingredientes,
                    preparacion = mercaderiaARemover.Preparacion,
                    imagen = mercaderiaARemover.Imagen,
                };
            }
            catch (ExceptionNotFound ex) { throw new ExceptionNotFound("Error en la búsqueda del id: " + ex.Message); }
            catch (Conflict ex) { throw new Conflict("Error en la base de datos: " + ex.Message); }
            catch (ExceptionSintaxError) { throw new ExceptionSintaxError("Sintaxis incorrecta para el Id"); }
        }


        public async Task<MercaderiaResponse> GetMercaderiaById(int mercaderiaId)
        {
            try
            {
                if (await VerifyHTTP404Async(mercaderiaId)) { throw new ExceptionNotFound("No existe una mercadería con ese Id"); }

                Mercaderia unaMercaderia = await _query.GetMercaderiaById(mercaderiaId);
                return new MercaderiaResponse
                {
                    id = unaMercaderia.MercaderiaID,
                    nombre = unaMercaderia.Nombre,
                    tipo = new TipoMercaderiaResponse
                    {
                        descripcion = _serviceTipMer.GetTipoMercaderiaById(unaMercaderia.TipoMercaderiaId).Result.Descripcion,
                        id = unaMercaderia.TipoMercaderiaId,
                    },
                    precio =(double) unaMercaderia.Precio,
                    ingredientes = unaMercaderia.Ingredientes,
                    preparacion = unaMercaderia.Preparacion,
                    imagen = unaMercaderia.Imagen,

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

                if (!int.TryParse(tipoMercaderia.ToString(), out int tipoMercaderiaId) && tipoMercaderia != null) { throw new ExceptionSintaxError("El valor de 'Tipo' no es válido, pruebe ingresar un número entero"); }

                if (orden == null) { orden = "ASC"; }

                if (orden.ToUpper() != "ASC" && orden.ToUpper() != "DESC") { throw new ExceptionSintaxError("El orden tiene que ser ingresado como 'desc' o 'asc'"); }
                
                if (nombre != null) { await GenerateMercaderiaResponseByName(listaMerGetResponse, (string)nombre); }

                if (tipoMercaderiaId >= 1 && tipoMercaderiaId <= await _serviceTipMer.GetCantidadTipoMercaderias())
                { 
                    if (listaMerGetResponse.Count != 0) { listaMerGetResponse = VerifyTipMerInList(listaMerGetResponse, (int)tipoMercaderiaId); }

                    else
                    {
                        List<Mercaderia> listaMercaderia = await _query.GetListMercaderia();
                        await GenerateMercaderiaResponseByTipoMercaderia(listaMercaderia, listaMerGetResponse, (int)tipoMercaderiaId);
                    }  
                }

                if (nombre == null && tipoMercaderia == null)
                {
                    List<Mercaderia> listaMercaderia = await _query.GetListMercaderia();
                    foreach (Mercaderia unaMercaderia in listaMercaderia)
                    { await GenerateMercaderiaResponse(unaMercaderia, listaMerGetResponse); }
                }

                if (orden.ToUpper().Equals("ASC")) { listaMerGetResponse = listaMerGetResponse.OrderBy(m => m.Precio).ToList(); }
                if (orden.ToUpper().Equals("DESC")) { listaMerGetResponse = listaMerGetResponse.OrderByDescending(m => m.Precio).ToList(); }

                return listaMerGetResponse;
            }
            catch (ExceptionSintaxError ex) { throw new ExceptionSintaxError("Error en la sintaxis: "+ ex.Message); }
            catch (ExceptionNotFound ex) { throw new ExceptionSintaxError("Error: " + ex.Message); }
        }

        //Métodos Para GetMercaderiaByFilter
        private async Task GenerateMercaderiaResponse(Mercaderia unaMercaderia, List<MercaderiaGetResponse> listaMerGetResponse)
        {
            var unaMercaderiaGetResponse = new MercaderiaGetResponse
            {
                id = unaMercaderia.MercaderiaID,
                nombre = unaMercaderia.Nombre,
                Precio = (double)unaMercaderia.Precio,
                tipo = new TipoMercaderiaResponse
                {
                    id = unaMercaderia.TipoMercaderiaId,
                    descripcion = (await _serviceTipMer.GetTipoMercaderiaById(unaMercaderia.TipoMercaderiaId)).Descripcion
                },
                imagen = unaMercaderia.Imagen
            };
            if (!listaMerGetResponse.Any(m => m.id == unaMercaderiaGetResponse.id)) { listaMerGetResponse.Add(unaMercaderiaGetResponse); }
        }
        private async Task GenerateMercaderiaResponseByTipoMercaderia(List<Mercaderia> listaMercaderia, List<MercaderiaGetResponse> listaMerGetResponse, int tipoMercaderia)
        {
            foreach (Mercaderia unaMercaderia in listaMercaderia)
            {
                if (tipoMercaderia == unaMercaderia.TipoMercaderiaId) { await GenerateMercaderiaResponse(unaMercaderia, listaMerGetResponse); }
            }
        }
        private async Task GenerateMercaderiaResponseByName(List<MercaderiaGetResponse> listaMerGetResponse, string nombre)
        {
            List<Mercaderia>listaMercaderia = await SearchProduct(nombre);
            foreach (Mercaderia unaMercaderia in listaMercaderia) { await GenerateMercaderiaResponse(unaMercaderia, listaMerGetResponse);  }
        }

        private List<MercaderiaGetResponse> VerifyTipMerInList(List<MercaderiaGetResponse> listaMerGetResponse, int tipoMercaderiaId)
        {
            List<MercaderiaGetResponse> newList = new List<MercaderiaGetResponse>();

            foreach (MercaderiaGetResponse unaMercaderiaGetResponse in listaMerGetResponse)
            {
                if (unaMercaderiaGetResponse.tipo.id == tipoMercaderiaId) { newList.Add(unaMercaderiaGetResponse); }
            }

            return newList;
        }

        private async Task<List<Mercaderia>> SearchProduct(string nombre) { return await _query.SearchLikeName(nombre); }

        //Métodos para el error HTTP 409

        private async Task<bool> VerifyHTTP409InsertAsync(Mercaderia unaMercaderia)
        {
            List<Mercaderia> listaMercaderias = await _query.GetListMercaderia();
            foreach (Mercaderia mercaderia in listaMercaderias)
            {
                if (mercaderia.Nombre.Equals(unaMercaderia.Nombre)) { return true; }
            }

            return false;
        }
        private async Task<bool> VerifyHTTP409ModifyAsync(string nombreMercaderia, int mercaderiaId)
        {
            List<Mercaderia> listaMercaderias = await _query.GetListMercaderia();
            foreach (Mercaderia mercaderia in listaMercaderias)
            {
                if (mercaderia.Nombre.Equals(nombreMercaderia) && mercaderia.MercaderiaID != mercaderiaId) { return true; }
            }

            return false;
        }

        //Método para el error HTTP 404
        private async Task<bool> VerifyHTTP404Async(int mercaderiaId)
        {
            if (await _query.GetMercaderiaById(mercaderiaId) == null) { return true; }
            else { return false; }
            
        }
        private async Task<bool> VerifyIfExistInComanda(int mercaderiaId)
        {
            List<Comanda> ListaComandas = await _comandaQuery.GetListComanda();
            foreach (Comanda unaComanda in ListaComandas)
            {
                foreach (ComandaMercaderia unaComandaMercaderia in unaComanda.ComandasMercaderia)
                { if (mercaderiaId == unaComandaMercaderia.MercaderiaId) { return true; } }
            }

            return false;
        }

        //Método para el error HTTP 400
        private async Task Verify400Sintax(MercaderiaRequest mercaderiaRequest)
        {
            if (string.IsNullOrEmpty(mercaderiaRequest.nombre)) { throw new ExceptionSintaxError("El nombre no es válido"); }
            
            if (!int.TryParse(mercaderiaRequest.tipo.ToString(), out int tipoMercaderia)) { throw new ExceptionSintaxError("El tipo de mercaderia no es un valor válido, pruebe introduciendo un entero"); }
            
            if (mercaderiaRequest.tipo < 0 || mercaderiaRequest.tipo > await _serviceTipMer.GetCantidadTipoMercaderias())  { throw new ExceptionSintaxError("El tipo de mercaderia no es un valor válido, pruebe introduciendo un entero"); }

            if (!int.TryParse(mercaderiaRequest.precio.ToString(), out int precio) || mercaderiaRequest.precio < 0) { throw new ExceptionSintaxError("El precio no es un valor válido, pruebe introduciendo un entero mayor a 0"); }
            
            if (string.IsNullOrEmpty(mercaderiaRequest.ingredientes)) { throw new ExceptionSintaxError("Los ingredientes no son válidos"); }
            
            if (string.IsNullOrEmpty(mercaderiaRequest.preparacion)) { throw new ExceptionSintaxError("La preparación no es válida"); }
            
            if (string.IsNullOrEmpty(mercaderiaRequest.imagen)) { throw new ExceptionSintaxError("La imagen no es válida"); }
        }
    }
}
