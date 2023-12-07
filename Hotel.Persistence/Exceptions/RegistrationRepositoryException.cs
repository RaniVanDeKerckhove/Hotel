using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Exceptions
{
    public class RegistrationRepositoryException  : Exception
    {

        public RegistrationRepositoryException(string? message) : base(message)
        {
        }

        public RegistrationRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    
    }
}
