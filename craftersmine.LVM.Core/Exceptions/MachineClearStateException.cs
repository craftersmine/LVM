using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Exceptions
{
    /// <summary>
    /// Thrown when <see cref="Machine.ClearState"/> being called while machine is running (<see cref="Machine.IsRunning"/> is true)
    /// </summary>

    [Serializable]
    public class MachineClearStateException : Exception
    {
#pragma warning disable CS1591
        public MachineClearStateException() { }
        public MachineClearStateException(string message) : base(message) { }
        public MachineClearStateException(string message, Exception inner) : base(message, inner) { }
        protected MachineClearStateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
#pragma warning restore CS1591
    }
}
