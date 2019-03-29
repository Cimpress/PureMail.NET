using System;

namespace Cimpress.PureMail.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message)
            : base(message)
        {
           
        }
       
    }
}
