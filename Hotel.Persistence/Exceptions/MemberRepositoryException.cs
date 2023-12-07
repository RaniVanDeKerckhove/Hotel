using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Exceptions
{
    public class MemberRepositoryException: Exception
    {
        public MemberRepositoryException(string? message) : base(message)
        {
        }

        public MemberRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
  
    }
}
