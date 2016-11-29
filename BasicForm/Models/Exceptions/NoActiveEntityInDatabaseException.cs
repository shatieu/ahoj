using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models.Exceptions
{
    public class NoActiveEntityInDatabaseException : System.Exception
    {
        public NoActiveEntityInDatabaseException() : base() { }
        public NoActiveEntityInDatabaseException(string message) : base(message) { }
        public NoActiveEntityInDatabaseException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected NoActiveEntityInDatabaseException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}