using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miotec.Vert3d.DomainModel
{
    
    public class InvalidLogException : Exception
    {
      
        public InvalidLogException(string message, string logName, Exception innerException)
            : base(message, innerException)
        {
            this.LogName = logName;
        }

      
        public InvalidLogException(string message, string logName)
            : this(message, logName, null)
        {
        }

     
        public string LogName { get; private set; }
    }
}
