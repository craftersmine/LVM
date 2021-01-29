using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Exceptions
{

    [Serializable]
    public class DeviceLoadException : Exception
    {
        public DeviceLoadException() { }
        public DeviceLoadException(string message) : base(message) { }
        public DeviceLoadException(string message, Exception inner) : base(message, inner) { }
        protected DeviceLoadException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
