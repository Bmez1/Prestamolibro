using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PruebaIngresoblibliotecario.Core.DTOs.Inputs;
using PruebaIngresoblibliotecario.Core.utils.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using Xunit;

namespace PruebaIngresoBibliotecario.Api.CustomTest
{
    public class PrestamoTesting
    {
        private readonly HttpClient _testClient;

        public PrestamoTesting()
        {
            WebApplicationFactory<Startup> appFactory = new WebApplicationFactory<PruebaIngresoBibliotecario.Api.Startup>();
            _testClient = appFactory.CreateClient();
        }

        /// <summary>
        /// Prueba unitaria para verificar que el sistema maneje correctamente el caso de error al intentar prestar
        /// el mismo libro a usuarios válidos que ya tienen el libro prestado.
        /// </summary>
        [Fact]
        public void CrearPrestamoMismoLibroError()
        {
            TipoUsuario[] usuarios = { TipoUsuario.EMPLEADO, TipoUsuario.AFILIADO }; // Válido solo para este tipo de ususarios

            foreach (TipoUsuario usuario in usuarios)
            {
                Guid isbnLibro = Guid.NewGuid();
                string identifiacionUsuario = "12345" + (int)usuario;
                string msjError = $"El libro con ISBN {isbnLibro} ya está prestado";
                HttpResponseMessage respuesta = null;
                try
                {
                    var solicitudPrestamo = new CrearPrestamoDto
                    {
                        TipoUsuario = usuario,
                        IdentificacionUsuario = identifiacionUsuario,
                        Isbn = isbnLibro
                    };

                    respuesta = _testClient.PostAsync("api/prestamo", solicitudPrestamo, new JsonMediaTypeFormatter()).Result;
                    respuesta.EnsureSuccessStatusCode();

                    respuesta = _testClient.PostAsync("api/prestamo", solicitudPrestamo, new JsonMediaTypeFormatter()).Result;
                    respuesta.EnsureSuccessStatusCode();

                    Assert.True(false, "No deberia permitir prestar el libro ya que está prestado");
                }
                catch (Exception)
                {
                    var contenidoRespuesta = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(respuesta.Content.ReadAsStringAsync().Result);
                    contenidoRespuesta["mensaje"].Should().Be(msjError);
                }

            }

        }


        /// <summary>
        /// Prueba unitaria para verificar que el sistema permita prestar el mismo libro nuevamente con éxito
        /// después de devolverlo, para diferentes tipos de usuarios (EMPLEADO, AFILIADO, INVITADO).
        /// </summary>
        [Fact]
        public void CrearPrestamoMismoLibroExito()
        {
            TipoUsuario[] usuarios = { TipoUsuario.EMPLEADO, TipoUsuario.AFILIADO, TipoUsuario.INVITADO };

            foreach (TipoUsuario usuario in usuarios)
            {
                Guid isbnLibro = Guid.NewGuid();
                string identifiacionUsuario = "12345" + (int)usuario;
                string msjError = $"El libro con ISBN {isbnLibro} ya está prestado";
                HttpResponseMessage respuesta = null;
                try
                {
                    CrearPrestamoDto solicitudPrestamo = new CrearPrestamoDto
                    {
                        TipoUsuario = usuario,
                        IdentificacionUsuario = identifiacionUsuario,
                        Isbn = isbnLibro
                    };

                    DevolverPrestamoInput devolverPrestamo = new DevolverPrestamoInput
                    {
                        IdentificacionUsuario = identifiacionUsuario,
                        Isbn = isbnLibro
                    };

                    string jsonData = JsonConvert.SerializeObject(devolverPrestamo);
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    respuesta = _testClient.PostAsync("api/prestamo", solicitudPrestamo, new JsonMediaTypeFormatter()).Result;
                    respuesta.EnsureSuccessStatusCode();

                    respuesta = _testClient.PatchAsync("api/prestamo", content).Result;
                    respuesta.EnsureSuccessStatusCode();

                    respuesta = _testClient.PostAsync("api/prestamo", solicitudPrestamo, new JsonMediaTypeFormatter()).Result;

                    Assert.True(respuesta.IsSuccessStatusCode, "Libro prestado, devuelto y prestado nuevamente con éxito");
                }
                catch (Exception)
                {
                    var contenidoRespuesta = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(respuesta.Content.ReadAsStringAsync().Result);
                    contenidoRespuesta["mensaje"].Should().Be(msjError);
                }

            }

        }
    }
}
