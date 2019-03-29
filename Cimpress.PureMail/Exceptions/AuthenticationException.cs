using System;

namespace Cimpress.PureMail.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message)
            : base(message)
        {
           
        }
       
    }
}
