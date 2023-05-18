using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class Conflict : Exception
    {
 

        public Conflict(string message) : base(message)
        {
        }

        public Conflict(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
