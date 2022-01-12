using System;

namespace Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(): base("No Access")
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