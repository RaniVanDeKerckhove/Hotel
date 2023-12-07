using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Exceptions
{
    public class OrganizerRepositoryException : Exception
    {

        public OrganizerRepositoryException(string? message) : base(message)
        {
        }

        public OrganizerRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }


    }
}
