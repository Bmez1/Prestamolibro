using Microsoft.AspNetCore.Mvc;
using PruebaIngresoblibliotecario.Core.DTOs;
using PruebaIngresoblibliotecario.Core.DTOs.Inputs;
using PruebaIngresoblibliotecario.Core.Exceptions;
using PruebaIngresoblibliotecario.Core.Interfaces.Services;
using PruebaIngresoblibliotecario.Core.utils;
using System;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        /// <summary>
        /// Finalidad: Obtener la información de un prestamo a través del Id
        /// </summary>
        /// <param name="idPrestamo">Identificador único del prestamo</param>
        /// <returns></returns>
        [HttpGet("{idPrestamo}")]
        public async Task<IActionResult> GetprestamoById(Guid idPrestamo)
        {
            var response = await _prestamoService.GetByIdAsync(idPrestamo);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound(new { Mensaje = $"El prestamo con id {idPrestamo} no existe" });
        }

        /// <summary>
        /// Finalidad: Registrar un nuevo prestamo asociado a un tipo de usuario
        /// </summary>
        /// <param name="prestamoDTO">Información del prestamo a registrar</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PrestamoCreadoDto>> CrearPrestamo(CrearPrestamoDto prestamoInput)
        {
            try
            {
                PrestamoCreadoDto response = await _prestamoService.CreateAsync(prestamoInput);
                return Ok(response);
            }
            catch (ExistePrestamoException ex)
            {
                return BadRequest(new { Mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Finalidad: Actualiza el estado del prestamo de un libro
        /// </summary>
        /// <param name="prestamoInput"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> DevolverPrestamo([FromBody] DevolverPrestamoInput prestamoInput)
        {
            try
            {
                Respuesta response = await _prestamoService.DevolverPrestamo(prestamoInput);
                return Ok(response);
            }
            catch (ExistePrestamoException ex)
            {
                return BadRequest(new { Mensaje = ex.Message });
            }
        }
    }
}