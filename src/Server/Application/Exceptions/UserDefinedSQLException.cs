using System;

namespace Application.Exceptions
{
    public class UserDefinedSQLException : Exception
    {
        public UserDefinedSQLException(): base("An Sql error occurred, check logs")
        {
        }

        public UserDefinedSQLException(string message)
            : base(message)
        {
            
        }
        

        public UserDefinedSQLException(string message, Exception inner)
            : base(message, inner)
        {
        }
    
        
    }
}