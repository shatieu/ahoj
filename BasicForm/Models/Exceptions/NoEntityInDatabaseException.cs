using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models.Exceptions
{
    public class NoEntityInDatabaseException : System.Exception
    {
        public NoEntityInDatabaseException() : base() { }
        public NoEntityInDatabaseException(string message) : base(message) { }
        public NoEntityInDatabaseException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected NoEntityInDatabaseException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}