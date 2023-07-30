using PruebaIngresoblibliotecario.Core.DTOs;
using PruebaIngresoblibliotecario.Core.DTOs.Inputs;
using PruebaIngresoblibliotecario.Core.Entities;
using PruebaIngresoblibliotecario.Core.utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaIngresoblibliotecario.Core.Interfaces.Services
{
    public interface IPrestamoService
    {
        Task<IEnumerable<Prestamo>> GetAllAsync();

        Task<DataPrestamoDto> GetByIdAsync(Guid id);

        Task<PrestamoCreadoDto> CreateAsync(CrearPrestamoDto prestamoDTO);

        Task<Respuesta> DevolverPrestamo(DevolverPrestamoInput prestamo);
    }
}