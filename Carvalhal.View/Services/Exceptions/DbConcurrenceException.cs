using System;

namespace Carvalhal.View.Services.Exceptions
{
    public class DbConcurrenceException : ApplicationException
    {
        public DbConcurrenceException(string message) : base(message)
        {
        }
    }
}
