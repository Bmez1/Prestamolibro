using AutoMapper;
using PruebaIngresoblibliotecario.Core.DTOs;
using PruebaIngresoblibliotecario.Core.DTOs.Inputs;
using PruebaIngresoblibliotecario.Core.Entities;
using PruebaIngresoblibliotecario.Core.Exceptions;
using PruebaIngresoblibliotecario.Core.Interfaces.Repository;
using PruebaIngresoblibliotecario.Core.Interfaces.Services;
using PruebaIngresoblibliotecario.Core.utils;
using PruebaIngresoblibliotecario.Core.utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIngresoblibliotecario.Core.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IMapper _mapper;

        public PrestamoService(IPrestamoRepository prestamoRepository, IMapper mapper)
        {
            _prestamoRepository = prestamoRepository;
            _mapper = mapper;
        }

        public async Task<PrestamoCreadoDto> CreateAsync(CrearPrestamoDto prestamoDTO)
        {
            Prestamo prestamo = _mapper.Map<Prestamo>(prestamoDTO);

            // Validación tipo usuario inivitado
            if (prestamo.TipoUsuario == TipoUsuario.INVITADO)
            {
                IEnumerable<Prestamo> listaPrestamoByUsuario = await _prestamoRepository.GetAllAsync(itemPrestamo
                    => itemPrestamo.IdentificacionUsuario == prestamo.IdentificacionUsuario);

                if (listaPrestamoByUsuario.Any() && listaPrestamoByUsuario.Any(itemPrestamo => !itemPrestamo.IsDevuelto))
                {
                    throw new ExistePrestamoException($"El usuario con identificacion {prestamo.IdentificacionUsuario} " +
                    $"ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo");
                }
            }
            else
            {
                IEnumerable<Prestamo> listaPrestamosByISBN = await _prestamoRepository.GetAllAsync(itemPrestamo
                    => itemPrestamo.Isbn == prestamo.Isbn && !itemPrestamo.IsDevuelto);

                if (listaPrestamosByISBN.Any())
                {
                    throw new ExistePrestamoException($"El libro con ISBN {prestamo.Isbn} ya está prestado");
                }
            }

            GetFechaMaximaPrestamo(prestamo);

            await _prestamoRepository.CreateAsync(prestamo);

            return _mapper.Map<PrestamoCreadoDto>(prestamo);
        }

        public async Task<Respuesta> DevolverPrestamo(DevolverPrestamoInput prestamo)
        {
            Prestamo prestamoModel = await _prestamoRepository.GetPrestamoFindByISBN_Identificacion(prestamo);

            if (prestamoModel is null)
            {
                throw new ExistePrestamoException("No existen prestamos asociados a la información suministrada");
            }

            prestamoModel.IsDevuelto = true;

            await _prestamoRepository.UpdateAsync(prestamoModel);

            return new Respuesta
            {
                Mensaje = "Libro devuleto con exito"
            };
        }

        public async Task<IEnumerable<Prestamo>> GetAllAsync() => await _prestamoRepository.GetAllAsync();

        public async Task<DataPrestamoDto> GetByIdAsync(Guid id)
        {
            Prestamo prestamo = await _prestamoRepository.GetByIdAsync(id);
            DataPrestamoDto response = _mapper.Map<DataPrestamoDto>(prestamo); 
            return response;
        }

        /// <summary>
        /// Finalidad: Calcula fecha máxima en el que un usuario debe tener un libro y
        /// le da el valor a la propidad del objeto prestamo
        /// </summary>
        /// <param name="prestamo">Objeto que representa la información del prestamo</param>
        private void GetFechaMaximaPrestamo(Prestamo prestamo)
        {
            int maximoDiasPrestamo = DiasPrestamo.DiasPrestamoByUsuario[prestamo.TipoUsuario];
            DateTime fechaPrestamo = prestamo.FechaPrestamo;

            Func<DayOfWeek, int> agregarDia = dia => dia == DayOfWeek.Sunday || dia == DayOfWeek.Saturday ? 0 : 1;

            int cantidadDiasEncontrados = 0;

            while (cantidadDiasEncontrados < maximoDiasPrestamo)
            {
                fechaPrestamo = fechaPrestamo.AddDays(1);
                cantidadDiasEncontrados += agregarDia(fechaPrestamo.DayOfWeek);
            }

            prestamo.FechaMaximaDevolucion = fechaPrestamo;
        }
    }
}