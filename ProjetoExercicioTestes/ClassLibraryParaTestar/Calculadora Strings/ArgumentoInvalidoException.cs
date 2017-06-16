using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryParaTestar
{
    [Serializable()]
    public class ArgumentoInvalidoException : System.Exception
    {
        public ArgumentoInvalidoException() : base() {}
        public ArgumentoInvalidoException(String message) : base(message) {}
        public ArgumentoInvalidoException(String message, System.Exception inner) : base(message,inner) {}

        protected ArgumentoInvalidoException(System.Runtime.Serialization.SerializationInfo info, 
                                             System.Runtime.Serialization.StreamingContext context) {}
    }
}
