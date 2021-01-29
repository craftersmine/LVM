using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Exceptions
{
    /// <summary>
    /// Thrown when user-defined Lua error thrown
    /// </summary>
    [Serializable]
    public class LuaMachineException : Exception
    {
        /// <summary>
        /// Gets Lua formatted error message
        /// </summary>
        public string LuaErrorMessage { get; private set; }
#pragma warning disable CS1591
        public LuaMachineException() { }
        public LuaMachineException(string message) : base(message) { LuaErrorMessage = "generic error"; }
        public LuaMachineException(string message, string luaError) : base(message) { LuaErrorMessage = luaError; }
        public LuaMachineException(string message, Exception inner) : base(message, inner) { }
        protected LuaMachineException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
#pragma warning restore CS1591
    }
}
