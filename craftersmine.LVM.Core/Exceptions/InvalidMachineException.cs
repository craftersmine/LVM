using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Exceptions
{

    [Serializable]
    public class InvalidMachineException : Exception
    {
        public InvalidMachineException() { }
        public InvalidMachineException(string message) : base(message) { }
        public InvalidMachineException(string message, Exception inner) : base(message, inner) { }
        protected InvalidMachineException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
