using System;

namespace Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(): base("User Unauthorized")
        {
            
        }

        public UnauthorizedException(string message)
            : base(message)
        {
            
        }
        

        public UnauthorizedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    
        
    }
}