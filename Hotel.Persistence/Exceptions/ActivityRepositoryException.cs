using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Exceptions
{
    public class ActivityRepositoryException : Exception
    {

        public ActivityRepositoryException(string? message) : base(message)
        {
        }

        public ActivityRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
   
    }
}
