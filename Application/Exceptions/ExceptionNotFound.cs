using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ExceptionNotFound: Exception
    {
        public ExceptionNotFound(string message) : base(message)
        {
        }

        public ExceptionNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
