using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miotec.Vert3d.DomainModel
{
    public class LoggingInitializationException : Exception
    {
        public LoggingInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public LoggingInitializationException(string message)
            : this(message, null)
        {
        }
    }
}
