using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class ActivityException : Exception
    {

        public ActivityException(string? message) : base(message)
        {
        }

        public ActivityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
