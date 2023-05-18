using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TP2_Individual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IComandaService _service;

        public ComandaController(IComandaService service)
        {
            _service = service;
        }
        [HttpGet]
        [ProducesResponseType(typeof(ComandaResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public async Task<IActionResult> GetComandaByDate(string fecha)
        {
            try
            {
                var result = await _service.GetAllComandaByDate(fecha);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ComandaResponse), 201)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public async Task<IActionResult> RegisterComanda(ComandaRequest request)
        {
            try
            {
                var result = await _service.RegisterComanda(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }

        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ComandaGetResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public async Task<IActionResult> GetComandaById(Guid Id)
        {
            try
            {
                var result = await _service.GetComandaById(Id);
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
    }
}
