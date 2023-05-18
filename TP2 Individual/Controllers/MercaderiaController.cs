using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace TP2_Individual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MercaderiaController : ControllerBase
    {
        private readonly IMercaderiaService _service;
        public MercaderiaController(IMercaderiaService service)
        {
            _service = service;
        }


        [HttpGet]
        [ProducesResponseType(typeof(MercaderiaGetResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public async Task<IActionResult> GetMercaderiaByfilter(int? tipo, string? nombre, string? orden)
        {
            try
            {
                var result = await _service.GetMercaderiaByFilter(tipo, nombre, orden);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
        }



        [HttpPost]
        [ProducesResponseType(typeof(MercaderiaResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public async Task<IActionResult> RegisterMercaderia(MercaderiaRequest request)
        {
            try
            {
                var result = await _service.RegisterMercaderia(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (Conflict ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 409 };
            }
        }


        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(MercaderiaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public async Task<IActionResult> GetMercaderiaById(int Id)
        {
            try
            {
                var result = await _service.GetMercaderiaById(Id);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (ExceptionNotFound ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
            }

        }


        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(MercaderiaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public async Task<IActionResult> ModifyMercaderia(int Id, MercaderiaRequest mercaderia)
        {
            try
            {
                var result = await _service.ModifyMercaderia(Id, mercaderia);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (ExceptionNotFound ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
            }
            catch (Conflict ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 409 };
            }
        }



        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(MercaderiaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public async Task<IActionResult> DeleteMercaderia(int Id)
        {
            try
            {
                var result = await _service.DeleteMercaderia(Id);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (Conflict ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 409 };
            }
        }
    }
}


