using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Exceptions
{
    /// <summary>
    /// Thrown when undefined <see cref="LuaProcessor"/> error occurs
    /// </summary>
    [Serializable]
    public class LuaProcessorException : Exception
    {
#pragma warning disable CS1591
        public LuaProcessorException() { }
        public LuaProcessorException(string message) : base(message) { }
        public LuaProcessorException(string message, Exception inner) : base(message, inner) { }
        protected LuaProcessorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
#pragma warning restore CS1591
    }
}
