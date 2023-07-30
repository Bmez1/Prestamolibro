using System;

namespace PruebaIngresoblibliotecario.Core.Exceptions
{
    public class ExistePrestamoException : Exception
    {
        public ExistePrestamoException(string message) : base(message)
        {
        }
    }
}