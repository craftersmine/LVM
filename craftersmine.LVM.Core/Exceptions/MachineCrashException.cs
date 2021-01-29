using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Exceptions
{
    /// <summary>
    /// Thrown when machine is halted due crash
    /// </summary>
    [Serializable]
    public class MachineCrashException : Exception
    {
#pragma warning disable CS1591
        public MachineCrashException() { }
        public MachineCrashException(string message) : base(message) { }
        public MachineCrashException(string message, Exception inner) : base(message, inner) { }
        protected MachineCrashException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
#pragma warning restore CS1591
    }
}
